using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class PurchaseItem : BaseEntity
{
    public int BookId { get; set; }

    [Required]
    [ForeignKey(nameof(BookId))]
    public virtual Book? Book { get; set; }

    public int CartId { get; set; }

    [Required]
    [ForeignKey(nameof(CartId))]
    public virtual Cart? Cart { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Count must be non-negative.")]
    public int Count { get; set; }
}
