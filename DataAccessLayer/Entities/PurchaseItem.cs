using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class PurchaseItem : BaseEntity
{
    public required int BookId { get; set; }

    [ForeignKey(nameof(BookId))]
    public virtual Book Book { get; set; }

    public int CartId { get; set; }

    [ForeignKey(nameof(CartId))]
    public virtual Cart Cart { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public required int Count { get; set; }
}
