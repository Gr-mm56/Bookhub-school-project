using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;

public class WishlistItem : BaseEntity
{
    public required int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    [JsonIgnore]
    public virtual User User { get; set; }

    public required int BookId { get; set; }

    [ForeignKey(nameof(BookId))]
    [JsonIgnore]
    public virtual Book Book { get; set; }
}
