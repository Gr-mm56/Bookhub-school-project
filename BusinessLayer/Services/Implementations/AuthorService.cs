using BusinessLayer.Mappers;
using BusinessLayer.Models.Author.Requests;
using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Common;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class AuthorService : BaseService<BookHubDbContext>, IAuthorService
{
    public AuthorService(BookHubDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<PagedResultDto<AuthorBooksDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Authors
            .AsNoTracking()
            .Include(a => a.ProfilePhoto)
            .Include(a => a.Books)
                .ThenInclude(b => b.Image)
            .OrderBy(a => a.Surname);

        return await PageAsync<Author, AuthorBooksDto>(
            query, 
            limit, 
            offset, 
            authors => AuthorMapper.ToDetailDtoList(authors)
        );
    }

    public async Task<AuthorBooksDto?> GetByIdAsync(int id)
    {
        var author = await Context.Authors
            .AsNoTracking()
            .Include(a => a.ProfilePhoto)
            .Include(a => a.Books)
                .ThenInclude(b => b.Image)
            .FirstOrDefaultAsync(a => a.Id == id);

        return author != null ? AuthorMapper.ToDetailDto(author) : null;
    }

    public async Task<AuthorBooksDto> CreateAsync(AuthorRequestDto requestDto)
    {
        // Validate that all provided IDs exist
        await ValidateRelatedEntitiesExistAsync(requestDto);

        var author = AuthorMapper.CreateEntity(requestDto);

        // Handle optional ProfilePhotoId
        if (requestDto.ProfilePhotoId <= 0)
        {
            author.ProfilePhotoId = null;
        }

        // Load related entities and associate them with the author
        await AssociateRelatedEntitiesAsync(author, requestDto);

        await Context.Authors.AddAsync(author);
        await SaveAsync();

        // Reload with all related data
        var createdAuthor = await Context.Authors
            .Include(a => a.Books)
            .Include(a => a.Books)
                .ThenInclude(b => b.Image)
            .FirstAsync(a => a.Id == author.Id);

        return AuthorMapper.ToDetailDto(createdAuthor);
    }

    public async Task<AuthorBooksDto?> UpdateAsync(int id, AuthorRequestDto requestDto)
    {
        var author = await Context.Authors
            .Include(a => a.ProfilePhoto)
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

        // Handle optional ProfilePhotoId
        if (requestDto.ProfilePhotoId <= 0)
        {
            author.ProfilePhotoId = null;
        }

        await ExtendBooksCollectionAsync(author, requestDto);

        await SaveAsync();

        // Reload with updated data
        var updatedAuthor = await Context.Authors
            .Include(a => a.ProfilePhoto)
            .Include(a => a.Books)
                .ThenInclude(b => b.Image)
            .FirstAsync(a => a.Id == id);

        return AuthorMapper.ToDetailDto(updatedAuthor);
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

    private async Task ExtendBooksCollectionAsync(Author author, AuthorRequestDto requestDto)
    {
        if (requestDto.BookIds.Any())
        {
            var currentBookIds = author.Books.Select(b => b.Id).ToList();

            var newBookIds = requestDto.BookIds.Except(currentBookIds).ToList();

            if (newBookIds.Any())
            {
                var newBooks = await Context.Books
                    .Where(b => newBookIds.Contains(b.Id))
                    .ToListAsync();

                foreach (var book in newBooks)
                {
                    author.Books.Add(book);
                }
            }
        }
    }

    public async Task<bool> DeleteAsync(int id)
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

        if (author.Books.Any())
        {
            throw new InvalidOperationException("Cannot delete author who has associated books. Remove the author from all books first.");
        }

        Context.Authors.Remove(author);
        await SaveAsync();
        return true;
    }
}
