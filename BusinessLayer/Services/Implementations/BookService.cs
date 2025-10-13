using BusinessLayer.Mappers;
using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Common;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class BookService(BookHubDbContext context) : BaseService<BookHubDbContext>(context), IBookService
{
    public async Task<PagedResultDto<BookDto>> GetBooksAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Books
            .AsNoTracking()
            .Include(b => b.Image)
            .OrderBy(b => b.Title);

        return await PageAsync(query, limit, offset, BookMapper.ToDtoList);
    }

    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        var book = await Context.Books
            .AsNoTracking()
            .Include(b => b.Image)
            .FirstOrDefaultAsync(b => b.Id == id);

        return book != null ? BookMapper.ToDto(book) : null;
    }

    public async Task<BookDetailDto?> GetBookDetailAsync(int id)
    {
        var book = await Context.Books
            .AsNoTracking()
            .Include(b => b.Image)
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(b => b.Id == id);

        return book != null ? BookMapper.ToDetailDto(book) : null;
    }

    public async Task<PagedResultDto<BookDetailDto>> SearchBooksAsync(BookSearchDto searchDto)
    {
        var query = Context.Books
            .AsNoTracking()
            .Include(b => b.Image)
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .Include(b => b.Publisher)
            .AsQueryable();

        // Apply filters
        if (!string.IsNullOrEmpty(searchDto.Title))
        {
            query = query.Where(b => b.Title.Contains(searchDto.Title.Trim()));
        }

        if (!string.IsNullOrEmpty(searchDto.Description))
        {
            query = query.Where(b => b.Description != null && b.Description.Contains(searchDto.Description.Trim()));
        }

        if (!string.IsNullOrEmpty(searchDto.Author))
        {
            query = query.Where(b => b.Authors.Any(a => a.Name.Contains(searchDto.Author.Trim())));
        }

        if (!string.IsNullOrEmpty(searchDto.Genre))
        {
            query = query.Where(b => b.Genres.Any(g => g.Name.Contains(searchDto.Genre.Trim())));
        }

        if (!string.IsNullOrEmpty(searchDto.Publisher))
        {
            query = query.Where(b => b.Publisher != null);
        }

        if (searchDto.Price.HasValue)
        {
            query = query.Where(b => b.Price == searchDto.Price.Value);
        }

        query = query.OrderBy(b => b.Title);

        return await PageAsync(query, searchDto.Limit, searchDto.Offset, BookMapper.ToDetailDtoList);
    }
    public async Task<BookDetailDto> CreateBookAsync(BookRequestDto requestDto)
    {
        // Validate that all provided IDs exist
        await ValidateRelatedEntitiesExistAsync(requestDto);

        var book = BookMapper.ToEntity(requestDto);
        
        // Load related entities and associate them with the book
        await AssociateRelatedEntitiesAsync(book, requestDto);
        
        await Context.Books.AddAsync(book);
        await SaveAsync();
        
        // Reload with all related data to return complete DetailDto
        var createdBook = await Context.Books
            .Include(b => b.Image)
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .Include(b => b.Publisher)
            .FirstAsync(b => b.Id == book.Id);
        
        return BookMapper.ToDetailDto(createdBook);
    }
    public async Task<BookDetailDto?> UpdateBookAsync(int id, BookRequestDto requestDto)
    {
        var book = await Context.Books
            .Include(b => b.Image)
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(b => b.Id == id);
        
        if (book == null)
            return null;

        // Validate that all provided IDs exist
        await ValidateRelatedEntitiesExistAsync(requestDto);

        // Update basic properties
        BookMapper.UpdateEntity(book, requestDto);
        
        // Clear existing relationships and add new ones
        book.Authors.Clear();
        book.Genres.Clear();
        
        // Load and associate new related entities
        await AssociateRelatedEntitiesAsync(book, requestDto);
        
        await SaveAsync();
        
        return BookMapper.ToDetailDto(book);
    }

    private async Task ValidateRelatedEntitiesExistAsync(BookRequestDto requestDto)
    {
        var errors = new List<string>();

        // Validate Authors
        if (requestDto.AuthorIds.Any())
        {
            var existingAuthorIds = await Context.Authors
                .Where(a => requestDto.AuthorIds.Contains(a.Id))
                .Select(a => a.Id)
                .ToListAsync();
            
            var invalidAuthorIds = requestDto.AuthorIds.Except(existingAuthorIds);
            var invalidAuthorList = invalidAuthorIds.ToList();
            if (invalidAuthorList.Any())
            {
                errors.Add($"Invalid Author IDs: {string.Join(", ", invalidAuthorList)}");
            }
        }

        // Validate Genres
        if (requestDto.GenreIds.Any())
        {
            var existingGenreIds = await Context.Genres
                .Where(g => requestDto.GenreIds.Contains(g.Id))
                .Select(g => g.Id)
                .ToListAsync();
            
            var invalidGenreIds = requestDto.GenreIds.Except(existingGenreIds);
            var invalidGenreList = invalidGenreIds.ToList();
            if (invalidGenreList.Any())
            {
                errors.Add($"Invalid Genre IDs: {string.Join(", ", invalidGenreList)}");
            }
        }

        // Validate Publishers
        if (requestDto.PublisherId > 0)
        {
            var publisherExists = await Context.Publishers
                .AnyAsync(p => p.Id == requestDto.PublisherId);
            
            if (!publisherExists)
            {
                errors.Add($"Invalid Publisher ID: {requestDto.PublisherId}");
            }
        }

        // Validate Image
        if (requestDto.ImageId > 0)
        {
            var imageExists = await Context.Images.AnyAsync(i => i.Id == requestDto.ImageId);
            if (!imageExists)
            {
                errors.Add($"Invalid Image ID: {requestDto.ImageId}");
            }
        }

        if (errors.Any())
        {
            throw new ArgumentException($"Validation failed: {string.Join("; ", errors)}");
        }
    }

    private async Task AssociateRelatedEntitiesAsync(Book book, BookRequestDto requestDto)
    {
        // Load and associate Authors
        if (requestDto.AuthorIds.Any())
        {
            var authors = await Context.Authors
                .Where(a => requestDto.AuthorIds.Contains(a.Id))
                .ToListAsync();
            book.Authors = authors;
        }

        // Load and associate Genres
        if (requestDto.GenreIds.Any())
        {
            var genres = await Context.Genres
                .Where(g => requestDto.GenreIds.Contains(g.Id))
                .ToListAsync();
            book.Genres = genres;
        }
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await Context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
            return false;

        Context.Books.Remove(book);
        await SaveAsync();
        return true;
    }
}