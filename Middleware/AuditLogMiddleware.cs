using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Middleware;

public class AuditLogMiddleware
{
    private const string CONTROLLER_ROUTE_KEY = "controller";
    private const string ENTITY_ID_ROUTE_KEY = "id";
    private readonly RequestDelegate _next;

    public AuditLogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, BookHubDbContext dbContext)
    {
        var httpRequest = httpContext.Request;
        await _next(httpContext);
        
        var entityId = ExtractEntityId(httpRequest);
        var modificationDetails = await ExtractModificationDetailsAsync(httpRequest);

        var editCount = await GetPreviousEditCountAsync(dbContext, entityId);
        if (httpRequest.Method == "PUT")
        {
            editCount += 1;
        }

        var auditLog = new AuditLog
        {
            Username = httpContext.User.Identity?.Name ?? String.Empty,
            EntityId = entityId,
            EntityName = httpRequest.RouteValues[CONTROLLER_ROUTE_KEY]?.ToString() ?? "Unknown",
            Action = httpRequest.Method,
            ModifiedAt = DateTime.UtcNow,
            ModificationDetails = modificationDetails,
            EditCount = editCount
        };
        dbContext.AuditLogs.Add(auditLog);
        await dbContext.SaveChangesAsync();
    }

    private static int ExtractEntityId(HttpRequest request)
    {
        request.RouteValues.TryGetValue(ENTITY_ID_ROUTE_KEY, out var idValue);
        return int.TryParse(idValue?.ToString(), out var id) ? id : 0;
    }

    private static async Task<string> ExtractModificationDetailsAsync(HttpRequest request)
    {
        var modificationDetails = string.Empty;

        switch (request.Method)
        {
            case "POST":
            case "PUT":
                modificationDetails = await ReadRequestBodyAsync(request);
                break;
            case "DELETE":
                request.RouteValues.TryGetValue(ENTITY_ID_ROUTE_KEY, out var idValueObj);
                modificationDetails = (string?)idValueObj ?? string.Empty;
                break;
        }
        return modificationDetails;

    }

    private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering();
        var originalPosition = request.Body.Position;
        try
        {

            request.Body.Position = 0;
            using var streamReader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            return await streamReader.ReadToEndAsync();
        }
        finally
        {
            request.Body.Position = originalPosition;
        }
    }

    private static async Task<int> GetPreviousEditCountAsync(BookHubDbContext database, int entityId)
    {
        var lastAuditEntry = await database.AuditLogs
            .Where(log => log.EntityId == entityId &&
                         log.Action == "PUT")
            .FirstOrDefaultAsync();

        return lastAuditEntry?.EditCount ?? 0;
    }
}
