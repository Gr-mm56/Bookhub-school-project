using BusinessLayer.Models.GiftCardCoupon.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mappers;

public static class GiftCardCouponMapper
{
    public static GiftCardCouponDto ToDto(GiftCardCoupon coupon)
    {
        ArgumentNullException.ThrowIfNull(coupon);

        return new GiftCardCouponDto
        {
            Id = coupon.Id,
            Code = coupon.Code,
            GiftCardId = coupon.GiftCardId,
            UsedInOrderId = coupon.UsedInOrderId,
            CreatedAt = coupon.CreatedAt,
            UpdatedAt = coupon.UpdatedAt
        };
    }

    public static GiftCardCouponDetailDto ToDetailDto(GiftCardCoupon coupon)
    {
        ArgumentNullException.ThrowIfNull(coupon);

        return new GiftCardCouponDetailDto
        {
            Id = coupon.Id,
            Code = coupon.Code,
            GiftCardId = coupon.GiftCardId,
            GiftCard = coupon.GiftCard != null ? GiftCardMapper.ToDto(coupon.GiftCard) : null,
            UsedInOrderId = coupon.UsedInOrderId,
            UsedInOrder = coupon.UsedInOrder != null ? CartMapper.ToDto(coupon.UsedInOrder) : null,
            CreatedAt = coupon.CreatedAt,
            UpdatedAt = coupon.UpdatedAt
        };
    }

    public static GiftCardCoupon CreateEntity(string code, int giftCardId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        return new GiftCardCoupon
        {
            Code = code,
            GiftCardId = giftCardId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static IEnumerable<GiftCardCouponDto> ToDtoList(IEnumerable<GiftCardCoupon> coupons)
    {
        return coupons.Select(ToDto);
    }

    public static IEnumerable<GiftCardCouponDetailDto> ToDetailDtoList(IEnumerable<GiftCardCoupon> coupons)
    {
        return coupons.Select(ToDetailDto);
    }
}