using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer.Services;

public interface IAuditLogService
{
    List<AuditLog> GenerateAuditLogs(IEnumerable<EntityEntry> entries, string currentUsername, DbSet<AuditLog> auditLogs);
}