using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.WishlistItem.Requests;
using BusinessLayer.Models.WishlistItem.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class WishlistItemService : BaseService<BookHubDbContext>, IWishlistItemService
{
    public WishlistItemService(BookHubDbContext context) : base(context)
    {
    }

    public async Task<PagedResultDto<WishlistItemDetailDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var query = Context.WishlistItems
            .AsNoTracking()
            .Include(w => w.Book)
            .Include(w => w.User)
            .OrderBy(w => w.Id);

        return await PageAsync(query, limit, offset, WishlistItemMapper.ToDetailDtoList);
    }

    public async Task<WishlistItemDetailDto?> GetByIdAsync(int id)
    {
        var wishlistItem = await Context.WishlistItems
            .AsNoTracking()
            .Include(w => w.Book)
            .Include(w => w.User)
            .FirstOrDefaultAsync(w => w.Id == id);

        return wishlistItem != null ? WishlistItemMapper.ToDetailDto(wishlistItem) : null;
    }

    public async Task<List<WishlistItemDetailDto>> GetWishlistByUserIdAsync(int userId)
    {
        var wishlistItems = await Context.WishlistItems
            .AsNoTracking()
            .Include(w => w.Book)
            .Include(w => w.User)
            .Where(w => w.UserId == userId)
            .ToListAsync();

        return WishlistItemMapper.ToDetailDtoList(wishlistItems).ToList();;
    }

    public async Task<WishlistItemDetailDto> CreateAsync(WishlistItemCreateDto wishlistItemCreateDto)
    {
        // Validate that User and Book exist
        await ValidateRelatedEntitiesExistAsync(wishlistItemCreateDto);

        var wishlistItem = WishlistItemMapper.CreateDtoToEntity(wishlistItemCreateDto);

        await Context.WishlistItems.AddAsync(wishlistItem);
        await SaveAsync();

        wishlistItem = await Context.WishlistItems
            .Include(w => w.Book)
            .FirstOrDefaultAsync(w => w.Id == wishlistItem.Id);

        return WishlistItemMapper.ToDetailDto(wishlistItem);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var wishlistItem = await Context.WishlistItems.FirstOrDefaultAsync(w => w.Id == id);
        if (wishlistItem == null)
        {
            return false;
        }

        Context.WishlistItems.Remove(wishlistItem);
        await SaveAsync();

        return true;
    }

    public async Task<bool> DeleteByUserBookIdAsync(int userId, int bookId)
    {
        var wishlistItem = await Context.WishlistItems
            .FirstOrDefaultAsync(w => w.UserId == userId && w.BookId == bookId);

        if (wishlistItem == null)
        {
            return false;
        }

        Context.WishlistItems.Remove(wishlistItem);
        await SaveAsync();

        return true;
    }

    public Task<WishlistItemDetailDto?> UpdateAsync(int id, WishlistItemCreateDto dto)
    {
        // Wishlist items cannot be updated.
        return Task.FromResult<WishlistItemDetailDto?>(null);
    }

    private async Task ValidateRelatedEntitiesExistAsync(WishlistItemCreateDto wishlistItemDto)
    {
        var errors = new List<string>();

        // Validate User exists
        var userExists = await Context.Users.AnyAsync(u => u.Id == wishlistItemDto.UserId);
        if (!userExists)
        {
            errors.Add($"Invalid User ID: {wishlistItemDto.UserId}");
        }

        // Validate Book exists
        var bookExists = await Context.Books.AnyAsync(b => b.Id == wishlistItemDto.BookId);
        if (!bookExists)
        {
            errors.Add($"Invalid Book ID: {wishlistItemDto.BookId}");
        }

        if (errors.Count != 0)
        {
            throw new ArgumentException($"Validation failed: {string.Join("; ", errors)}");
        }
    }
}
