using BusinessLayer.Models.WishlistItem.Requests;
using BusinessLayer.Models.WishlistItem.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IWishlistItemService
    : ICrudService<WishlistItemDetailDto, WishlistItemDetailDto, WishlistItemCreateDto, WishlistItemCreateDto>
{
    Task<List<WishlistItemDetailDto>> GetWishlistByUserIdAsync(int userId);

    Task<bool> DeleteByUserBookIdAsync(int userId, int bookId);
}
