using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class GiftCard : BaseEntity
{
    [Required]
    [Range(0, double.MaxValue)]
    public required double PriceReduction { get; set; }

    [Required]
    public required DateTime ValidFrom { get; set; }

    [Required]
    public required DateTime ValidTo { get; set; }

    public virtual ICollection<GiftCardCoupon> Coupons { get; set; } = new List<GiftCardCoupon>();
}