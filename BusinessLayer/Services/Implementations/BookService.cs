using BusinessLayer.Mappers;
using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Common;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class BookService : BaseService<BookHubDbContext>, IBookService
{
    public BookService(BookHubDbContext dbContext) : base(dbContext)
    {

    }
    public async Task<PagedResultDto<BookDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Books
            .AsNoTracking()
            .Include(b => b.Image)
            .OrderBy(b => b.Title);

        return await PageAsync(query, limit, offset, BookMapper.ToDtoList);
    }

    public async Task<BookDetailDto?> GetByIdAsync(int id)
    {
        var book = await Context.Books
            .AsNoTracking()
            .Include(b => b.Image)
            .Include(b => b.PrimaryGenre)
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
            .Include(b => b.PrimaryGenre)
            .AsQueryable();

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
            var genreTerm = searchDto.Genre.Trim();
            // match either the PrimaryGenre name or any of the book's Genres
            query = query.Where(b => (b.PrimaryGenre != null && b.PrimaryGenre.Name.Contains(genreTerm))
                                     || b.Genres.Any(g => g.Name.Contains(genreTerm)));
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
            .Include(b => b.Image)
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .Include(b => b.Publisher)
            .FirstAsync(b => b.Id == book.Id);

        return BookMapper.ToDto(createdBook);
    }

    public async Task<BookDto?> UpdateAsync(int id, BookRequestDto requestDto)
    {
        var book = await Context.Books
            .Include(b => b.Image)
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return null;
        }
/*
        if (requestDto.PublisherId == 0)
        {
            requestDto.PublisherId = null;
        }*/

        await ValidateRelatedEntitiesExistAsync(requestDto);

        BookMapper.UpdateEntity(book, requestDto);

        book.Authors.Clear();
        book.Genres.Clear();

        await AssociateRelatedEntitiesAsync(book, requestDto);

        await SaveAsync();

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
            if (invalidAuthorList.Any())
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
            if (invalidGenreList.Any())
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
      /*  else if (requestDto.PublisherId < 0)
        {
            errors.Add($"Publisher ID must be provided and greater than or equal to 0");
        }*/
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
      /*  else if (requestDto.ImageId < 0)
        {
            errors.Add($"Image ID must be provided and greater than or equal to 0");

        }*/

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
        return true;
    }
}
