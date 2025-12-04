using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;

/**
 * Cart entity holds same functionality as Order.
 * Adding OrderId and OrderDate means that order has been completed with the items in the cart.
 */
public class Cart : BaseEntity
{
    public required int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "TotalValue must be non-negative.")]
    public required double TotalValue { get; set; }

    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual ICollection<PurchaseItem>? PurchaseItems { get; set; }
}
