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
    public int UserId { get; set; }

    [Required]
    [ForeignKey(nameof(UserId))]
    [JsonIgnore]
    public virtual User? User { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "TotalValue must be non-negative.")]
    public double TotalValue { get; set; }

    public int? OrderId { get; set; } = null;

    public DateTime? OrderDate { get; set; } = null;

    [JsonIgnore]
    public virtual ICollection<PurchaseItem>? PurchaseItems { get; set; }
}
