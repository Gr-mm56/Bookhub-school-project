using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class GiftCardCoupon : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public required string Code { get; set; }

    public required int GiftCardId { get; set; }

    [ForeignKey(nameof(GiftCardId))]
    public virtual GiftCard GiftCard { get; set; } = null!;

    public int? UsedInOrderId { get; set; }

    [ForeignKey(nameof(UsedInOrderId))]
    public virtual Cart? UsedInOrder { get; set; }

    // Helper property to check if coupon is used
    [NotMapped]
    public bool IsUsed => UsedInOrderId.HasValue;
}