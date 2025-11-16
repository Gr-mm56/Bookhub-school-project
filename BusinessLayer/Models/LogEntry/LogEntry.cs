using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusinessLayer.Models.LogEntry;

public class LogEntry
{
    [BsonId]
    public ObjectId Id { get; set; }

    public DateTime Timestamp { get; set; }
    public string Method { get; set; }
    public string Path { get; set; }
    public string QueryString { get; set; }
    public int StatusCode { get; set; }
    public long DurationMs { get; set; }
}
