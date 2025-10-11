using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;

public class PurchaseItem : BaseEntity
{
    public int BookId { get; set; }

    [Required]
    [ForeignKey(nameof(BookId))]
    [JsonIgnore]
    public virtual Book? Book { get; set; }

    public int CartId { get; set; }

    [Required]
    [ForeignKey(nameof(CartId))]
    [JsonIgnore]
    public virtual Cart? Cart { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Count must be non-negative.")]
    public int Count { get; set; }
}
