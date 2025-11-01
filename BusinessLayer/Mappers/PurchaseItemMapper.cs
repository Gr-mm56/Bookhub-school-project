using BusinessLayer.Models.PurchaseItem.Requests;
using BusinessLayer.Models.PurchaseItem.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mappers;

public static class PurchaseItemMapper
{
    public static PurchaseItemDto ToDto(PurchaseItem purchaseItem)
    {
        ArgumentNullException.ThrowIfNull(purchaseItem);

        return new PurchaseItemDto
        {
            Id = purchaseItem.Id,
            BookId = purchaseItem.BookId,
            CartId = purchaseItem.CartId,
            Count = purchaseItem.Count,
            CreatedAt = purchaseItem.CreatedAt,
            UpdatedAt = purchaseItem.UpdatedAt,
        };
    }

    public static PurchaseItemDetailDto ToDetailDto(PurchaseItem purchaseItem)
    {
        ArgumentNullException.ThrowIfNull(purchaseItem);

        return new PurchaseItemDetailDto
        {
            Id = purchaseItem.Id,
            BookId = purchaseItem.BookId,
            Book = purchaseItem.Book != null ? BookMapper.ToDto(purchaseItem.Book) : null,
            CartId = purchaseItem.CartId,
            Cart = CartMapper.ToDto(purchaseItem.Cart ?? new Cart()),
            Count = purchaseItem.Count,
            CreatedAt = purchaseItem.CreatedAt,
            UpdatedAt = purchaseItem.UpdatedAt,
        };
    }

    public static PurchaseItem CreateDtoToEntity(PurchaseItemCreateDto createDto)
    {
        ArgumentNullException.ThrowIfNull(createDto);

        return new PurchaseItem
        {
            BookId = createDto.BookId,
            CartId = createDto.CartId,
            Count = createDto.Count,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
    }

    public static void UpdateEntity(PurchaseItem purchaseItem, PurchaseItemUpdateDto updateDto)
    {
        ArgumentNullException.ThrowIfNull(purchaseItem);
        ArgumentNullException.ThrowIfNull(updateDto);

        purchaseItem.Count = updateDto.Count;
        purchaseItem.UpdatedAt = DateTime.Now;
    }

    public static IEnumerable<PurchaseItemDto> ToDtoList(IEnumerable<PurchaseItem> purchaseItems)
    {
        return purchaseItems.Select(ToDto);
    }

    public static IEnumerable<PurchaseItemDetailDto> ToDetailDtoList(IEnumerable<PurchaseItem> purchaseItems)
    {
        return purchaseItems.Select(ToDetailDto);
    }
}
