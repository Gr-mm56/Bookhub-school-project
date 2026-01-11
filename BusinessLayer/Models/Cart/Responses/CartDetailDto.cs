using BusinessLayer.Models.GiftCardCoupon.Responses;
using BusinessLayer.Models.PurchaseItem.Responses;
using BusinessLayer.Models.User.Responses;

namespace BusinessLayer.Models.Cart.Responses;

public class CartDetailDto : CartDto
{
    public UserDto User { get; set; }

    public ICollection<PurchaseItemDetailDto>? PurchaseItems { get; set; } = new List<PurchaseItemDetailDto>();

    public int? AppliedGiftCardCouponId { get; set; }

    public GiftCardCouponDetailDto? AppliedGiftCardCoupon { get; set; }
}
