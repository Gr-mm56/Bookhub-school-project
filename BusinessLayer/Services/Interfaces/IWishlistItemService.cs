using BusinessLayer.Models.WishlistItem.Requests;
using BusinessLayer.Models.WishlistItem.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IWishlistItemService
    : ICrudService<WishlistItemDetailDto, WishlistItemDetailDto, WishlistItemCreateDto, WishlistItemCreateDto>
{
}
