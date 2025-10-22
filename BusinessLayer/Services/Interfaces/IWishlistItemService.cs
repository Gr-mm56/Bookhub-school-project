using BusinessLayer.Models.Common;
using BusinessLayer.Models.WishlistItem.Requests;
using BusinessLayer.Models.WishlistItem.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IWishlistItemService : ICrudService<WishlistItemDetailDto, WishlistItemCreateDto, WishlistItemCreateDto>
{
    Task<PagedResultDto<WishlistItemDetailDto>> GetAllAsync(int limit = 20, int offset = 0);

    Task<WishlistItemDetailDto?> GetByIdAsync(int id);

    Task<WishlistItemDetailDto> CreateAsync(WishlistItemCreateDto requestDto);

    Task<bool> DeleteAsync(int id);
}
