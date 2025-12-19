using BusinessLayer.Mappers;
using BusinessLayer.Models.GiftCardCoupon.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class GiftCardCouponService : BaseService<BookHubDbContext>, IGiftCardCouponService
{
    public GiftCardCouponService(BookHubDbContext context) : base(context)
    {
    }

    public async Task<GiftCardCouponDetailDto?> ValidateAndGetCouponAsync(string code)
    {
        var coupon = await GetCouponWithDetailsAsync(code);

        if (coupon == null)
        {
            return null;
        }

        ValidateCouponUsage(coupon);
        ValidateCouponValidity(coupon);

        return GiftCardCouponMapper.ToDetailDto(coupon);
    }

    public async Task<bool> MarkAsUsedAsync(int couponId, int orderId)
    {
        var coupon = await Context.GiftCardCoupons.FindAsync(couponId);

        if (coupon == null)
        {
            return false;
        }

        ValidateCouponUsage(coupon);

        coupon.UsedInOrderId = orderId;
        coupon.UpdatedAt = DateTime.UtcNow;

        await SaveAsync();

        return true;
    }

    private async Task<GiftCardCoupon?> GetCouponWithDetailsAsync(string code)
    {
        return await Context.GiftCardCoupons
            .AsNoTracking()
            .Include(c => c.GiftCard)
            .Include(c => c.UsedInOrder)
            .FirstOrDefaultAsync(c => c.Code == code);
    }

    private static void ValidateCouponUsage(GiftCardCoupon coupon)
    {
        if (coupon.UsedInOrderId.HasValue)
        {
            throw new InvalidOperationException("This coupon has already been used.");
        }
    }

    private static void ValidateCouponValidity(GiftCardCoupon coupon)
    {
        var now = DateTime.UtcNow;

        if (now < coupon.GiftCard.ValidFrom)
        {
            throw new InvalidOperationException(
                $"This coupon is not yet valid. It will be valid from {coupon.GiftCard.ValidFrom:dd-mm-yyyy}.");
        }

        if (now > coupon.GiftCard.ValidTo)
        {
            throw new InvalidOperationException(
                $"This coupon has expired. It was valid until {coupon.GiftCard.ValidTo:dd-mm-yyyy}.");
        }
    }
}
