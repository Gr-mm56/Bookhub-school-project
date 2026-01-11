using BusinessLayer.Models.GiftCardCoupon.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IGiftCardCouponService
{
    Task<GiftCardCouponDetailDto?> ValidateAndGetCouponAsync(string code);
    Task<bool> MarkAsUsedAsync(int couponId, int orderId);
}