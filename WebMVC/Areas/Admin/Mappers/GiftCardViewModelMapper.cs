using BusinessLayer.Models.GiftCard.Requests;
using BusinessLayer.Models.GiftCard.Responses;
using WebMVC.Areas.Admin.Models.GiftCard;

namespace WebMVC.Areas.Admin.Mappers;

public static class GiftCardViewModelMapper
{
    public static GiftCardViewModel ToListViewModel(ICollection<GiftCardCouponsDto> giftCardDtos)
    {
        ArgumentNullException.ThrowIfNull(giftCardDtos);

        return new GiftCardViewModel
        {
            GiftCards = giftCardDtos
                .OrderByDescending(x => x.CreatedAt)
                .Select(ToListItemViewModel)
                .ToList()
        };
    }

    public static GiftCardListItemViewModel ToListItemViewModel(GiftCardCouponsDto giftCardDto)
    {
        ArgumentNullException.ThrowIfNull(giftCardDto);

        var totalCoupons = giftCardDto.Coupons?.Count ?? 0;
        var usedCoupons = giftCardDto.Coupons?.Count(c => c.IsUsed) ?? 0;
        var now = DateTime.UtcNow;
        var isActive = now >= giftCardDto.ValidFrom && now <= giftCardDto.ValidTo;

        return new GiftCardListItemViewModel
        {
            Id = giftCardDto.Id,
            PriceReduction = giftCardDto.PriceReduction,
            ValidFrom = giftCardDto.ValidFrom,
            ValidTo = giftCardDto.ValidTo,
            TotalCoupons = totalCoupons,
            UsedCoupons = usedCoupons,
            AvailableCoupons = totalCoupons - usedCoupons,
            IsActive = isActive,
            CreatedAt = giftCardDto.CreatedAt,
            UpdatedAt = giftCardDto.UpdatedAt
        };
    }

    public static GiftCardCreateDto ToCreateDto(GiftCardCreateEditViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return new GiftCardCreateDto
        {
            PriceReduction = viewModel.PriceReduction,
            ValidFrom = viewModel.ValidFrom,
            ValidTo = viewModel.ValidTo,
            NumberOfCoupons = viewModel.NumberOfCoupons
        };
    }

    public static GiftCardUpdateDto ToUpdateDto(GiftCardCreateEditViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return new GiftCardUpdateDto
        {
            PriceReduction = viewModel.PriceReduction,
            ValidFrom = viewModel.ValidFrom,
            ValidTo = viewModel.ValidTo,
            AdditionalCoupons = viewModel.AdditionalCoupons
        };
    }

    public static GiftCardCreateEditViewModel DtoToCreateEditViewModel(GiftCardCouponsDto giftCardDto)
    {
        ArgumentNullException.ThrowIfNull(giftCardDto);

        return new GiftCardCreateEditViewModel
        {
            PriceReduction = giftCardDto.PriceReduction,
            ValidFrom = giftCardDto.ValidFrom,
            ValidTo = giftCardDto.ValidTo,
            NumberOfCoupons = giftCardDto.Coupons?.Count ?? 0,
            AdditionalCoupons = 0
        };
    }

    public static GiftCardDeleteViewModel ToDeleteViewModel(GiftCardCouponsDto giftCardDto)
    {
        ArgumentNullException.ThrowIfNull(giftCardDto);

        return new GiftCardDeleteViewModel
        {
            Id = giftCardDto.Id,
            PriceReduction = giftCardDto.PriceReduction,
            ValidFrom = giftCardDto.ValidFrom,
            ValidTo = giftCardDto.ValidTo,
            TotalCoupons = giftCardDto.Coupons?.Count ?? 0,
            UsedCoupons = giftCardDto.Coupons?.Count(c => c.IsUsed) ?? 0,
            Coupons = giftCardDto.Coupons?.Select(c => new CouponInfoViewModel
            {
                Code = c.Code,
                IsUsed = c.IsUsed,
                UsedInOrderId = c.UsedInOrderId
            }).ToList() ?? new List<CouponInfoViewModel>(),
            CreatedAt = giftCardDto.CreatedAt,
            UpdatedAt = giftCardDto.UpdatedAt
        };
    }
}