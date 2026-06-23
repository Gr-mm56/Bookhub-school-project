namespace BusinessLayer.Models.GiftCardCoupon.Requests;

public class CouponOrderRequest
{
    public int CouponId { get; set; }
    public int OrderId { get; set; }
}