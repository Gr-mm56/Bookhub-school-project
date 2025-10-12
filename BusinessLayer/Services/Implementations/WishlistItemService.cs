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

    public async Task<PagedResultDto<WishlistItemDto>> GetWishlistItemsAsync(int limit = 20, int offset = 0)
    {
        var query = Context.WishlistItems
            .AsNoTracking()
            .OrderBy(u => u.Id);

        return await PageAsync(query, limit, offset, WishlistItemMapper.ToDtoList);
    }

    public async Task<WishlistItemDto?> GetWishlistItemByIdAsync(int id)
    {
        var wishlistItem = await Context.WishlistItems
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return wishlistItem != null ? WishlistItemMapper.ToDto(wishlistItem) : null;

    }

    public async Task<WishlistItemDto> CreateWishlistItemAsync(WishlistItemCreateDto wishlistItemCreateDto)
    {
        WishlistItem wishlistItem = WishlistItemMapper.CreateDtoToEntity(wishlistItemCreateDto);

        await Context.WishlistItems.AddAsync(wishlistItem);
        await SaveAsync();

        return WishlistItemMapper.ToDto(wishlistItem);

    }

    public async Task<bool> DeleteWishlistItemAsync(int id)
    {
        WishlistItem? wishlistItem = await Context.WishlistItems.FirstOrDefaultAsync(g => g.Id == id);
        if (wishlistItem == null)
            return false;

        Context.WishlistItems.Remove(wishlistItem);
        await SaveAsync();
        return true;
    }

    //
    // public async Task<WishlistItemDto?> UpdateWishlistItemAsync(int id, WishlistItemUpdateDto wishlistItemUpdateDto)
    // {
    //     WishlistItem? wishlistItem = await Context.WishlistItems.FirstOrDefaultAsync(u => u.Id == id);
    //     if (wishlistItem == null)
    //         return null;
    //
    //     WishlistItemMapper.UpdateEntity(wishlistItem, wishlistItemUpdateDto);
    //     await SaveAsync();
    //
    //     return WishlistItemMapper.ToDto(wishlistItem);
    // }
}
