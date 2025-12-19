using BusinessLayer.Models.GiftCard.Requests;
using BusinessLayer.Models.GiftCard.Responses;
using BusinessLayer.Models.GiftCardCoupon.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mappers;

public static class GiftCardMapper
{
    public static GiftCardDto ToDto(GiftCard giftCard)
    {
        ArgumentNullException.ThrowIfNull(giftCard);

        return new GiftCardDto
        {
            Id = giftCard.Id,
            PriceReduction = giftCard.PriceReduction,
            ValidFrom = giftCard.ValidFrom,
            ValidTo = giftCard.ValidTo,
            CreatedAt = giftCard.CreatedAt,
            UpdatedAt = giftCard.UpdatedAt
        };
    }

    public static GiftCardCouponsDto ToDetailDto(GiftCard giftCard)
    {
        ArgumentNullException.ThrowIfNull(giftCard);

        return new GiftCardCouponsDto
        {
            Id = giftCard.Id,
            PriceReduction = giftCard.PriceReduction,
            ValidFrom = giftCard.ValidFrom,
            ValidTo = giftCard.ValidTo,
            Coupons = giftCard.Coupons?.Select(GiftCardCouponMapper.ToDto).ToList() ?? new List<GiftCardCouponDto>(),
            CreatedAt = giftCard.CreatedAt,
            UpdatedAt = giftCard.UpdatedAt
        };
    }

    public static GiftCard CreateEntity(GiftCardCreateDto createDto)
    {
        ArgumentNullException.ThrowIfNull(createDto);

        return new GiftCard
        {
            PriceReduction = createDto.PriceReduction,
            ValidFrom = createDto.ValidFrom,
            ValidTo = createDto.ValidTo,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Coupons = new List<GiftCardCoupon>()
        };
    }

    public static void UpdateEntity(GiftCard giftCard, GiftCardUpdateDto updateDto)
    {
        ArgumentNullException.ThrowIfNull(giftCard);
        ArgumentNullException.ThrowIfNull(updateDto);

        giftCard.PriceReduction = updateDto.PriceReduction;
        giftCard.ValidFrom = updateDto.ValidFrom;
        giftCard.ValidTo = updateDto.ValidTo;
        giftCard.UpdatedAt = DateTime.UtcNow;
    }

    public static IEnumerable<GiftCardDto> ToDtoList(IEnumerable<GiftCard> giftCards)
    {
        return giftCards.Select(ToDto);
    }

    public static IEnumerable<GiftCardCouponsDto> ToDetailDtoList(IEnumerable<GiftCard> giftCards)
    {
        return giftCards.Select(ToDetailDto);
    }
}