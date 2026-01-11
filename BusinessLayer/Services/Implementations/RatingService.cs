using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Rating.Requests;
using BusinessLayer.Models.Rating.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Services.Implementations;

public class RatingService : BaseService<BookHubDbContext>, IRatingService
{
    private readonly IMemoryCache _memoryCache;
    private const string BookDetailCacheKey = "books_detail";

    public RatingService(BookHubDbContext context, IMemoryCache memoryCache) : base(context)
    {
        _memoryCache = memoryCache;
    }

    public async Task<PagedResultDto<RatingDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Ratings
            .AsNoTracking()
            .OrderByDescending(r => r.CreatedAt);

        return await PageAsync(query, limit, offset, RatingMapper.ToDtoList);
    }

    public async Task<RatingDetailDto?> GetByIdAsync(int id)
    {
        var rating = await Context.Ratings
            .AsNoTracking()
            .Include(r => r.Book)
            .ThenInclude(b => b!.Image)
            .FirstOrDefaultAsync(r => r.Id == id);

        return rating != null ? RatingMapper.ToDetailDto(rating) : null;
    }


    public async Task<PagedResultDto<RatingDto>> SearchRatingsAsync(RatingSearchDto searchDto)
    {
        var query = Context.Ratings
            .AsNoTracking()
            .AsQueryable();

        if (searchDto.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == searchDto.UserId.Value);
        }

        if (searchDto.BookId.HasValue)
        {
            query = query.Where(r => r.BookId == searchDto.BookId.Value);
        }

        if (searchDto.MinStars.HasValue)
        {
            query = query.Where(r => r.Stars >= searchDto.MinStars.Value);
        }

        if (searchDto.MaxStars.HasValue)
        {
            query = query.Where(r => r.Stars <= searchDto.MaxStars.Value);
        }

        query = query.OrderByDescending(r => r.CreatedAt);

        return await PageAsync(query, searchDto.Limit, searchDto.Offset, RatingMapper.ToDtoList);
    }

    public async Task<RatingDto> CreateAsync(RatingRequestDto requestDto)
    {
        await ValidateRelatedEntitiesExistAsync(requestDto);

        var existingRating = await Context.Ratings
            .FirstOrDefaultAsync(r => r.UserId == requestDto.UserId && r.BookId == requestDto.BookId);

        if (existingRating != null)
        {
            throw new ArgumentException($"User {requestDto.UserId} has already rated book {requestDto.BookId}");
        }

        var rating = RatingMapper.CreateEntity(requestDto);

        await Context.Ratings.AddAsync(rating);
        await SaveAsync();
        _memoryCache.Remove(BookDetailCacheKey);
        _memoryCache.Remove($"{BookDetailCacheKey}_{requestDto.BookId}");

        return RatingMapper.ToDto(rating);
    }

    public async Task<RatingDto?> UpdateAsync(int id, RatingRequestDto requestDto)
    {
        var rating = await Context.Ratings.FirstOrDefaultAsync(r => r.Id == id);
        if (rating == null)
        {
            return null;
        }

        await ValidateRelatedEntitiesExistAsync(requestDto);

        if ((rating.UserId != requestDto.UserId || rating.BookId != requestDto.BookId))
        {
            var existingRating = await Context.Ratings
                .FirstOrDefaultAsync(r => r.UserId == requestDto.UserId && r.BookId == requestDto.BookId && r.Id != id);

            if (existingRating != null)
            {
                throw new ArgumentException($"User {requestDto.UserId} has already rated book {requestDto.BookId}");
            }
        }

        RatingMapper.UpdateEntity(rating, requestDto);
        await SaveAsync();

        var oldBookId = rating.BookId;
        if (oldBookId != requestDto.BookId)
        {
            _memoryCache.Remove($"{BookDetailCacheKey}_{oldBookId}");
        }
        _memoryCache.Remove(BookDetailCacheKey);
        _memoryCache.Remove($"{BookDetailCacheKey}_{requestDto.BookId}");

        return RatingMapper.ToDto(rating);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rating = await Context.Ratings.FirstOrDefaultAsync(r => r.Id == id);
        if (rating == null)
        {
            return false;
        }

        var bookId = rating.BookId;
        
        Context.Ratings.Remove(rating);
        await SaveAsync();
        _memoryCache.Remove(BookDetailCacheKey);
        _memoryCache.Remove($"{BookDetailCacheKey}_{bookId}");
        
        return true;
    }

    private async Task ValidateRelatedEntitiesExistAsync(RatingRequestDto requestDto)
    {
        var errors = new List<string>();

        var userExists = await Context.Users.AnyAsync(u => u.Id == requestDto.UserId);
        if (!userExists)
        {
            errors.Add($"Invalid User ID: {requestDto.UserId}");
        }

        var bookExists = await Context.Books.AnyAsync(b => b.Id == requestDto.BookId);
        if (!bookExists)
        {
            errors.Add($"Invalid Book ID: {requestDto.BookId}");
        }

        if (errors.Count != 0)
        {
            throw new ArgumentException($"Validation failed: {string.Join("; ", errors)}");
        }
    }
}
