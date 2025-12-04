using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class AuditLog
{
    public int Id { get; set; }
    [MaxLength(50)]
    public required string Username { get; set; }
    [MaxLength(100)]
    public required string EntityName { get; set; }
    public required int EntityId { get; set; }
    [MaxLength(500)]
    public required string Action { get; set; }
    public DateTime ModifiedAt { get; set; }
    [MaxLength(2000)]
    public string ModificationDetails { get; set; } = "";
    public int EditCount { get; set; }
}
