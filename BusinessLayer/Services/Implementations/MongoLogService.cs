using BusinessLayer.Models.LogEntry;
using BusinessLayer.Services.Interfaces;
using MongoDB.Driver;

namespace BusinessLayer.Services.Implementations;

public class MongoLogService : ILogService
{
    private readonly IMongoCollection<LogEntry> _logsCollection;

    public MongoLogService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("BookHubLogs");
        _logsCollection = database.GetCollection<LogEntry>("request_logs");
    }

    public async Task LogRequestAsync(LogEntry logEntry)
    {
        await _logsCollection.InsertOneAsync(logEntry);
    }
}
