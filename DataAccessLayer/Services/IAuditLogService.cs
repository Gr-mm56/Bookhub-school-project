using DataAccessLayer.Entities;

namespace DataAccessLayer.Services;

public interface IAuditLogService
{
    List<AuditLog> GenerateAuditLogs(string currentUsername);
}