using BusinessLayer.Mappers;
using BusinessLayer.Models.Author.Requests;
using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Common;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class AuthorService(BookHubDbContext context) : BaseService<BookHubDbContext>(context), IAuthorService
{
    public async Task<PagedResultDto<AuthorDto>> GetAuthorsAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Authors
            .AsNoTracking()
            .Include(a => a.ProfilePhoto)
            .OrderBy(a => a.Surname);

        return await PageAsync(query, limit, offset, AuthorMapper.ToDtoList);
    }

    public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
    {
        var author = await Context.Authors
            .AsNoTracking()
            .Include(a => a.ProfilePhoto)
            .FirstOrDefaultAsync(a => a.Id == id);

        return author != null ? AuthorMapper.ToDto(author) : null;
    }

    public async Task<AuthorBooksDto?> GetAuthorBooksAsync(int id)
    {
        var author = await Context.Authors
            .AsNoTracking()
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);

        return author != null ? AuthorMapper.ToDetailDto(author) : null;
    }

    public async Task<AuthorBooksDto> CreateAuthorAsync(AuthorRequestDto requestDto)
    {
        // Validate that all provided IDs exist
        await ValidateRelatedEntitiesExistAsync(requestDto);

        var author = AuthorMapper.CreateEntity(requestDto);

        // Load related entities and associate them with the author
        await AssociateRelatedEntitiesAsync(author, requestDto);

        await Context.Authors.AddAsync(author);
        await SaveAsync();

        // Reload with all related data
        var createdAuthor = await Context.Authors
            .Include(a => a.Books)
            .FirstAsync(a => a.Id == author.Id);

        return AuthorMapper.ToDetailDto(createdAuthor);
    }

    public async Task<AuthorBooksDto?> UpdateAuthorAsync(int id, AuthorRequestDto requestDto)
    {
        var author = await Context.Authors
            .Include(b => b.Books)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            return null;
        }

        // Validate that all provided IDs exist
        await ValidateRelatedEntitiesExistAsync(requestDto);

        // Update basic properties
        AuthorMapper.UpdateEntity(author, requestDto);

        // Clear existing relationships and add new ones
        author.Books.Clear();

        // Load and associate new related entities
        await AssociateRelatedEntitiesAsync(author, requestDto);

        await SaveAsync();

        return AuthorMapper.ToDetailDto(author);
    }

    private async Task ValidateRelatedEntitiesExistAsync(AuthorRequestDto requestDto)
    {
        var errors = new List<string>();

        // Validate Books
        if (requestDto.BookIds.Any())
        {
            var existingBookIds = await Context.Books
                .Where(b => requestDto.BookIds.Contains(b.Id))
                .Select(b => b.Id)
                .ToListAsync();

            var invalidBookIds = requestDto.BookIds.Except(existingBookIds);
            var invalidBookList = invalidBookIds.ToList();
            if (invalidBookList.Any())
            {
                errors.Add($"Invalid Book IDs: {string.Join(", ", invalidBookList)}");
            }
        }

        // Validate Image
        if (requestDto.ProfilePhotoId > 0)
        {
            var imageExists = await Context.Images.AnyAsync(i => i.Id == requestDto.ProfilePhotoId);
            if (!imageExists)
            {
                errors.Add($"Invalid Image ID: {requestDto.ProfilePhotoId}");
            }
        }

        if (errors.Any())
        {
            throw new ArgumentException($"Validation failed: {string.Join("; ", errors)}");
        }
    }

    private async Task AssociateRelatedEntitiesAsync(Author author, AuthorRequestDto requestDto)
    {
        // Load and associate Books
        if (requestDto.BookIds.Any())
        {
            var books = await Context.Books
                .Where(a => requestDto.BookIds.Contains(a.Id))
                .ToListAsync();
            author.Books = books;
        }
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await Context.Authors
            .Include(a => a.Books)
                .ThenInclude(b => b.Authors)
            .Include(a => a.Books)
                .ThenInclude(b => b.Ratings)
            .Include(a => a.Books)
                .ThenInclude(b => b.Genres)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            return false;
        }

        Context.Authors.Remove(author);
        await SaveAsync();
        return true;
    }
}
