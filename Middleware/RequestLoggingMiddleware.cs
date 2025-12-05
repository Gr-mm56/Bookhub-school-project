using BusinessLayer.Models.LogEntry;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Diagnostics;

namespace Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogService _logService;

    public RequestLoggingMiddleware(RequestDelegate next, ILogService logService)
    {
        _next = next;
        _logService = logService;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();

        var source = context.Request.Path.StartsWithSegments("/api") ? "API" : "MVC";

        Log.Information(
            "[{Source}] Request processed: {Method} {Path} completed with status {StatusCode} in {DurationMs}ms",
            source,
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds
        );

        var logEntry = new LogEntry
        {
            Timestamp = DateTime.UtcNow,
            Method = context.Request.Method,
            Path = context.Request.Path,
            QueryString = context.Request.QueryString.ToString(),
            StatusCode = context.Response.StatusCode,
            DurationMs = stopwatch.ElapsedMilliseconds
        };

        _ = Task.Run(async () =>
        {
            try
            {
                await _logService.LogRequestAsync(logEntry);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to log to MongoDB");
            }
        });
    }
}
