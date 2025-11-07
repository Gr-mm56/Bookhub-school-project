using BusinessLayer.Models.WishlistItem.Requests;
using BusinessLayer.Models.WishlistItem.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mappers;

public static class WishlistItemMapper
{
    public static WishlistItemDto ToDto(WishlistItem wishlistItem)
    {
        ArgumentNullException.ThrowIfNull(wishlistItem);

        return new WishlistItemDto
        {
            Id = wishlistItem.Id,
            UserId = wishlistItem.UserId,
            BookId = wishlistItem.BookId,
            CreatedAt = wishlistItem.CreatedAt,
            UpdatedAt = wishlistItem.UpdatedAt
        };
    }

    public static WishlistItemDetailDto ToDetailDto(WishlistItem wishlistItem)
    {
        ArgumentNullException.ThrowIfNull(wishlistItem);

        return new WishlistItemDetailDto
        {
            Id = wishlistItem.Id,
            UserId = wishlistItem.UserId,
            User = UserMapper.ToDto(wishlistItem.User ?? new User()),
            BookId = wishlistItem.BookId,
            Book = BookMapper.ToDto(wishlistItem.Book ?? new Book()),
            CreatedAt = wishlistItem.CreatedAt,
            UpdatedAt = wishlistItem.UpdatedAt
        };
    }

    public static WishlistItem CreateDtoToEntity(WishlistItemCreateDto createDto)
    {
        ArgumentNullException.ThrowIfNull(createDto);

        return new WishlistItem
        {
            UserId = createDto.UserId,
            BookId = createDto.BookId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public static IEnumerable<WishlistItemDto> ToDtoList(IEnumerable<WishlistItem> wishlistItems)
    {
        return wishlistItems.Select(ToDto);
    }

    public static IEnumerable<WishlistItemDetailDto> ToDetailDtoList(IEnumerable<WishlistItem> wishlistItems)
    {
        return wishlistItems.Select(ToDetailDto);
    }
}
