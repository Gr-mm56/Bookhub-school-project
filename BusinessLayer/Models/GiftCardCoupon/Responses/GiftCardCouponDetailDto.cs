using BusinessLayer.Models.Cart.Responses;
using BusinessLayer.Models.GiftCard.Responses;

namespace BusinessLayer.Models.GiftCardCoupon.Responses;

public class GiftCardCouponDetailDto : GiftCardCouponDto
{
    public GiftCardDto? GiftCard { get; set; }
    public CartDto? UsedInOrder { get; set; }
}