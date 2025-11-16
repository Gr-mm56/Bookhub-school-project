using BusinessLayer.Models.LogEntry;

namespace BusinessLayer.Services.Interfaces;

public interface ILogService
{
    Task LogRequestAsync(LogEntry logEntry);
}
