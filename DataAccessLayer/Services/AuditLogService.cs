using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace DataAccessLayer.Services;

public class AuditLogService : IAuditLogService
{
    private readonly BookHubDbContext _dbContext;

    public AuditLogService(BookHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<AuditLog> GenerateAuditLogs(string currentUsername)
    {
        var auditLogs = new List<AuditLog>();

        foreach (var entry in _dbContext.ChangeTracker.Entries()
                 .Where(e => e.Entity is not AuditLog && e.State is not EntityState.Unchanged and not EntityState.Detached))
        {
            var entityId = ExtractEntityId(entry);
            if (entityId == 0)
            {
                continue;
            }

            var action = entry.State switch
            {
                EntityState.Added => "POST",
                EntityState.Modified => "PUT",
                EntityState.Deleted => "DELETE",
                _ => "UNKNOWN"
            };

            var modificationDetails = ExtractModificationDetails(entry, action);
            var editCount = GetPreviousEditCount(entityId, action);
            if (action == "PUT")
            {
                editCount += 1;
            }

            var auditLog = new AuditLog
            {
                Username = currentUsername,
                EntityId = entityId,
                EntityName = entry.Entity.GetType().Name,
                Action = action,
                ModifiedAt = DateTime.UtcNow,
                ModificationDetails = modificationDetails,
                EditCount = editCount
            };

            auditLogs.Add(auditLog);
        }

        return auditLogs;
    }

    private static int ExtractEntityId(EntityEntry entry)
    {
        var primaryKey = entry.Metadata.FindPrimaryKey();
        if (primaryKey?.Properties.Count != 1)
        {
            return 0;
        }

        var value = entry.CurrentValues[primaryKey.Properties[0]];
        return value is int id ? id : 0;
    }

    private static string ExtractModificationDetails(EntityEntry entry, string action)
    {
        try
        {
            var details = new Dictionary<string, object>();

            foreach (var property in entry.Properties)
            {
                var name = property.Metadata.Name;

                if (action == "POST")
                {
                    details[name] = property.CurrentValue ?? "null";
                }
                else if (action == "PUT" && property.IsModified)
                {
                    details[$"{name}_Old"] = property.OriginalValue ?? "null";
                    details[$"{name}_New"] = property.CurrentValue ?? "null";
                }
                else if (action == "DELETE")
                {
                    details[name] = property.OriginalValue ?? "null";
                }
            }

            return JsonSerializer.Serialize(details);
        }
        catch
        {
            return string.Empty;
        }
    }

    private int GetPreviousEditCount(int entityId, string action)
    {
        if (action != "PUT")
        {
            return 0;
        }

        var lastAuditEntry = _dbContext.AuditLogs
            .Where(log => log.EntityId == entityId && 
                        log.Action == "PUT")
            .FirstOrDefault();

        return lastAuditEntry?.EditCount ?? 0;
    }
}
