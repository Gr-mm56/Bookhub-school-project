using BusinessLayer.Mappers;
using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Common;
using BusinessLayer.Services.Extensions;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Services.Implementations;

public class BookService : BaseService<BookHubDbContext>, IBookService
{
    private readonly IMemoryCache _memoryCache;
    private const string BookSearchCacheKey = "books_search";
    private const string BookDetailCacheKey = "books_detail";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromSeconds(60);

    public BookService(BookHubDbContext dbContext, IMemoryCache memoryCache) : base(dbContext)
    {
        _memoryCache = memoryCache;
    }

    public async Task<PagedResultDto<BookDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Books
            .AsNoTracking()
            .WithListIncludes()
            .OrderBy(b => b.Title);

        return await PageAsync(query, limit, offset, BookMapper.ToDtoList);
    }

    public async Task<BookDetailDto?> GetByIdAsync(int id)
    {
        var cacheKey = $"{BookDetailCacheKey}_{id}";

        return await _memoryCache.GetOrCreateAsync(
            cacheKey,
            CacheExpiration,
            async () =>
            {
                var book = await Context.Books
                    .AsNoTracking()
                    .WithDetailIncludes()
                    .FirstOrDefaultAsync(b => b.Id == id);

                return book != null ? BookMapper.ToDetailDto(book) : null;
            }
        );
    }

    public async Task<PagedResultDto<BookDetailDto>> SearchBooksAsync(BookSearchDto searchDto)
    {
        var cacheKey =
            $"{BookSearchCacheKey}_{searchDto.SearchTerm ?? ""}_{searchDto.Price}_{searchDto.Limit}_{searchDto.Offset}";

        return await _memoryCache.GetOrCreateAsync(
            cacheKey,
            CacheExpiration,
            async () =>
            {
                var query = Context.Books
                    .AsNoTracking()
                    .WithBaseIncludes()
                    .AsQueryable();

                if (!string.IsNullOrEmpty(searchDto.SearchTerm))
                {
                    var searchTerm = searchDto.SearchTerm.Trim().ToLower();

                    query = query.Where(b =>
                        b.Title.ToLower().Contains(searchTerm) ||
                        b.Authors.Any(a => (a.Name.ToLower() + " " + a.Surname.ToLower()).Contains(searchTerm) ||
                                           a.Name.ToLower().Contains(searchTerm) ||
                                           a.Surname.ToLower().Contains(searchTerm)) ||
                        (b.Publisher != null && b.Publisher.Name.ToLower().Contains(searchTerm)) ||
                        (b.PrimaryGenre != null && b.PrimaryGenre.Name.ToLower().Contains(searchTerm)) ||
                        b.Genres.Any(g => g.Name.ToLower().Contains(searchTerm))
                    );
                }

                if (searchDto.Price.HasValue)
                {
                    query = query.Where(b => Math.Abs(b.Price - searchDto.Price.Value) <= 0.0001);
                }

                query = query.OrderBy(b => b.Title);

                return await PageAsync(query, searchDto.Limit, searchDto.Offset, BookMapper.ToDetailDtoList);
            }
        );
    }

    public async Task<BookDto> CreateAsync(BookRequestDto requestDto)
    {
        if (requestDto.GenreIds.Count == 0 || requestDto.AuthorIds.Count == 0)
        {
            throw new ArgumentException("You must provide at least one genre and author");
        }

        await ValidateRelatedEntitiesExistAsync(requestDto);
        if (requestDto.PublisherId == 0)
        {
            requestDto.PublisherId = null;
        }

        if (requestDto.ImageId == 0)
        {
            requestDto.ImageId = null;
        }

        var book = BookMapper.CreateEntity(requestDto);

        await AssociateRelatedEntitiesAsync(book, requestDto);

        await Context.Books.AddAsync(book);
        await SaveAsync();

        var createdBook = await Context.Books
            .WithBaseIncludes()
            .FirstAsync(b => b.Id == book.Id);

        _memoryCache.InvalidateAllCache();

        return BookMapper.ToDto(createdBook);
    }

    public async Task<BookDto?> UpdateAsync(int id, BookRequestDto requestDto)
    {
        var book = await Context.Books
            .WithBaseIncludes()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return null;
        }

        await ValidateRelatedEntitiesExistAsync(requestDto);

        BookMapper.UpdateEntity(book, requestDto);

        book.Authors.Clear();
        book.Genres.Clear();

        await AssociateRelatedEntitiesAsync(book, requestDto);

        await SaveAsync();

        _memoryCache.InvalidateAllCache();

        return BookMapper.ToDto(book);
    }

    private async Task ValidateRelatedEntitiesExistAsync(BookRequestDto requestDto)
    {
        var errors = new List<string>();

        if (requestDto.AuthorIds.Count != 0)
        {
            var existingAuthorIds = await Context.Authors
                .Where(a => requestDto.AuthorIds.Contains(a.Id))
                .Select(a => a.Id)
                .ToListAsync();

            var invalidAuthorIds = requestDto.AuthorIds.Except(existingAuthorIds);
            var invalidAuthorList = invalidAuthorIds.ToList();
            if (invalidAuthorList.Count != 0)
            {
                errors.Add($"Invalid Author IDs: {string.Join(", ", invalidAuthorList)}");
            }
        }

        // Validate Genres
        if (requestDto.GenreIds.Count != 0)
        {
            var existingGenreIds = await Context.Genres
                .Where(g => requestDto.GenreIds.Contains(g.Id))
                .Select(g => g.Id)
                .ToListAsync();

            var invalidGenreIds = requestDto.GenreIds.Except(existingGenreIds);
            var invalidGenreList = invalidGenreIds.ToList();
            if (invalidGenreList.Count != 0)
            {
                errors.Add($"Invalid Genre IDs: {string.Join(", ", invalidGenreList)}");
            }
        }

        if (requestDto.PublisherId > 0)
        {
            var publisherExists = await Context.Publishers
                .AnyAsync(p => p.Id == requestDto.PublisherId);

            if (!publisherExists)
            {
                errors.Add($"Invalid Publisher ID: {requestDto.PublisherId}");
            }
        }

        if (requestDto.PrimaryGenreId > 0)
        {
            var primaryGenreExists = await Context.Genres
                .AnyAsync(g => g.Id == requestDto.PrimaryGenreId);

            if (!primaryGenreExists)
            {
                errors.Add($"Invalid Primary Genre ID: {requestDto.PrimaryGenreId}");
            }
        }
        else
        {
            errors.Add($"Primary Genre ID must be provided and greater than 0");
        }

        if (requestDto.ImageId > 0)
        {
            var imageExists = await Context.Images.AnyAsync(i => i.Id == requestDto.ImageId);
            if (!imageExists)
            {
                errors.Add($"Invalid Image ID: {requestDto.ImageId}");
            }
        }

        if (errors.Count != 0)
        {
            throw new ArgumentException($"Validation failed: {string.Join("; ", errors)}");
        }
    }

    private async Task AssociateRelatedEntitiesAsync(Book book, BookRequestDto requestDto)
    {
        if (requestDto.AuthorIds.Count != 0)
        {
            var authors = await Context.Authors
                .Where(a => requestDto.AuthorIds.Contains(a.Id))
                .ToListAsync();
            book.Authors = authors;
        }

        if (requestDto.GenreIds.Count != 0)
        {
            var genres = await Context.Genres
                .Where(g => requestDto.GenreIds.Contains(g.Id))
                .ToListAsync();
            book.Genres = genres;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await Context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
        {
            return false;
        }

        Context.Books.Remove(book);
        await SaveAsync();

        _memoryCache.InvalidateAllCache();

        return true;
    }
}