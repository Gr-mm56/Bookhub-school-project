using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.WishlistItem.Requests;
using BusinessLayer.Models.WishlistItem.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class WishlistItemService : BaseService<BookHubDbContext>, IWishlistItemService
{
    public WishlistItemService(BookHubDbContext context): base(context)
    {
    }

    public async Task<PagedResultDto<WishlistItemDetailDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var query = Context.WishlistItems
            .AsNoTracking()
            .OrderBy(u => u.Id);

        return await PageAsync(query, limit, offset, WishlistItemMapper.ToDetailDtoList);
    }

    public async Task<WishlistItemDetailDto?> GetByIdAsync(int id)
    {
        var wishlistItem = await Context.WishlistItems
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return wishlistItem != null ? WishlistItemMapper.ToDetailDto(wishlistItem) : null;

    }

    public async Task<WishlistItemDetailDto> CreateAsync(WishlistItemCreateDto wishlistItemCreateDto)
    {
        WishlistItem wishlistItem = WishlistItemMapper.CreateDtoToEntity(wishlistItemCreateDto);

        await Context.WishlistItems.AddAsync(wishlistItem);
        await SaveAsync();

        return WishlistItemMapper.ToDetailDto(wishlistItem);

    }

    public async Task<bool> DeleteAsync(int id)
    {
        WishlistItem? wishlistItem = await Context.WishlistItems.FirstOrDefaultAsync(g => g.Id == id);
        if (wishlistItem == null)
            return false;

        Context.WishlistItems.Remove(wishlistItem);
        await SaveAsync();
        return true;
    }

    public Task<WishlistItemDetailDto?> UpdateAsync(int id, WishlistItemCreateDto dto)
    {
        // Wishlist items cannot be updated.
        return Task.FromResult<WishlistItemDetailDto?>(null);
    }
}
