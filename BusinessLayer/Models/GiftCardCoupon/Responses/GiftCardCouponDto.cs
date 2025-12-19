namespace BusinessLayer.Models.GiftCardCoupon.Responses;

public class GiftCardCouponDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int GiftCardId { get; set; }
    public int? UsedInOrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Computed property
    public bool IsUsed => UsedInOrderId.HasValue;
}