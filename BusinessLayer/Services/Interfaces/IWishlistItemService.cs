using BusinessLayer.Models.Common;
using BusinessLayer.Models.WishlistItem.Requests;
using BusinessLayer.Models.WishlistItem.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IWishlistItemService
{
    Task<PagedResultDto<WishlistItemDto>> GetWishlistItemsAsync(int limit = 20, int offset = 0);

    Task<WishlistItemDto?> GetWishlistItemByIdAsync(int id);

    Task<WishlistItemDto> CreateWishlistItemAsync(WishlistItemCreateDto requestDto);

    Task<bool> DeleteWishlistItemAsync(int id);
}
