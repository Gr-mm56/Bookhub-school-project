namespace DataAccessLayer.Entities;

public class AuditLog
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string EntityName { get; set; }
    public required int EntityId { get; set; }
    public required string Action { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string ModificationDetails { get; set; } = "";
    public int EditCount { get; set; }
}
