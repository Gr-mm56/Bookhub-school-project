using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Rating.Requests;
using BusinessLayer.Models.Rating.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class RatingService : BaseService<BookHubDbContext>, IRatingService
{
    public RatingService(BookHubDbContext context) : base(context)
    {
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
            .FirstOrDefaultAsync(r => r.Id == id);

        return rating != null ? RatingMapper.ToDetailDto(rating) : null;
    }

    public async Task<RatingDetailDto?> GetRatingDetailAsync(int id)
    {
        var rating = await Context.Ratings
            .AsNoTracking()
            .Include(r => r.Book)
            .ThenInclude(b => b.Image)
            .FirstOrDefaultAsync(r => r.Id == id);

        return rating != null ? RatingMapper.ToDetailDto(rating) : null;
    }

    public async Task<PagedResultDto<RatingDto>> SearchRatingsAsync(RatingSearchDto searchDto)
    {
        var query = Context.Ratings
            .AsNoTracking()
            .AsQueryable();

        // Apply filters
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
        // Validate that User and Book exist
        await ValidateRelatedEntitiesExistAsync(requestDto);

        // Check if user already rated this book
        var existingRating = await Context.Ratings
            .FirstOrDefaultAsync(r => r.UserId == requestDto.UserId && r.BookId == requestDto.BookId);

        if (existingRating != null)
        {
            throw new ArgumentException($"User {requestDto.UserId} has already rated book {requestDto.BookId}");
        }

        var rating = RatingMapper.ToEntity(requestDto);

        await Context.Ratings.AddAsync(rating);
        await SaveAsync();

        return RatingMapper.ToDto(rating);
    }

    public async Task<RatingDto?> UpdateAsync(int id, RatingRequestDto requestDto)
    {
        var rating = await Context.Ratings.FirstOrDefaultAsync(r => r.Id == id);
        if (rating == null)
        {
            return null;
        }

        // Validate that User and Book exist
        await ValidateRelatedEntitiesExistAsync(requestDto);

        // Check if trying to change to a different user/book combination that already has a rating
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

        return RatingMapper.ToDto(rating);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rating = await Context.Ratings.FirstOrDefaultAsync(r => r.Id == id);
        if (rating == null)
        {
            return false;
        }

        Context.Ratings.Remove(rating);
        await SaveAsync();
        return true;
    }

    private async Task ValidateRelatedEntitiesExistAsync(RatingRequestDto requestDto)
    {
        var errors = new List<string>();

        // Validate User exists
        var userExists = await Context.Users.AnyAsync(u => u.Id == requestDto.UserId);
        if (!userExists)
        {
            errors.Add($"Invalid User ID: {requestDto.UserId}");
        }

        // Validate Book exists
        var bookExists = await Context.Books.AnyAsync(b => b.Id == requestDto.BookId);
        if (!bookExists)
        {
            errors.Add($"Invalid Book ID: {requestDto.BookId}");
        }

        if (errors.Any())
        {
            throw new ArgumentException($"Validation failed: {string.Join("; ", errors)}");
        }
    }
}
