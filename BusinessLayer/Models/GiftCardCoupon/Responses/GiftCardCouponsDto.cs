using BusinessLayer.Models.GiftCardCoupon.Responses;

namespace BusinessLayer.Models.GiftCard.Responses;

public class GiftCardCouponsDto : GiftCardDto
{
    public ICollection<GiftCardCouponDto> Coupons { get; set; } = new List<GiftCardCouponDto>();
}