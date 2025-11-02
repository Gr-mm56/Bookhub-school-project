using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Publisher.Requests;
using BusinessLayer.Models.Publisher.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class PublisherService : BaseService<BookHubDbContext>, IPublisherService
{
    public PublisherService(BookHubDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<PagedResultDto<PublisherDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Publishers
            .AsNoTracking()
            .Include(p => p.ProfilePhoto)
            .OrderBy(p => p.Name);

        return await PageAsync(query, limit, offset, PublisherMapper.ToDtoList);
    }

    public async Task<PublisherBooksDto?> GetByIdAsync(int id)
    {
        var publisher = await Context.Publishers
            .AsNoTracking()
            .Include(p => p.ProfilePhoto)
            .FirstOrDefaultAsync(p => p.Id == id);

        return publisher != null ? PublisherMapper.ToDetailDto(publisher) : null;
    }

    public async Task<PublisherDto> CreateAsync(PublisherRequestDto requestDto)
    {
        // Validate that all provided IDs exist
        await ValidateRelatedEntitiesExistAsync(requestDto);

        var publisher = PublisherMapper.CreateEntity(requestDto);

        // Load related entities and associate them with the publisher
        await AssociateRelatedEntitiesAsync(publisher, requestDto);

        await Context.Publishers.AddAsync(publisher);
        await SaveAsync();

        // Reload with all related data
        var createdPublisher = await Context.Publishers
            .Include(p => p.Books)
            .FirstAsync(p => p.Id == publisher.Id);

        return PublisherMapper.ToDetailDto(createdPublisher);
    }

    public async Task<PublisherDto?> UpdateAsync(int id, PublisherRequestDto requestDto)
    {
        try
        {
            var publisher = await Context.Publishers
                .Include(p => p.Books)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (publisher == null)
            {
                return null;
            }

            // Validate that all provided IDs exist
            await ValidateRelatedEntitiesExistAsync(requestDto);

            // Update basic properties
            PublisherMapper.UpdateEntity(publisher, requestDto);

            try
            {
                // Ensure all requested books exist and check if they can be used before clearing
                if (requestDto.BookIds != null && requestDto.BookIds.Any())
                {
                    var existingBooks = await Context.Books
                        .Where(b => requestDto.BookIds.Contains(b.Id))
                        .ToListAsync();

                    if (existingBooks.Count != requestDto.BookIds.Count)
                    {
                        throw new ArgumentException("Some of the requested book IDs do not exist");
                    }

                    // Clear existing relationships and add new ones
                    // This is where the error was happening - we'll keep it but handle exceptions
                    publisher.Books.Clear();

                    // Load and associate new related entities
                    await AssociateRelatedEntitiesAsync(publisher, requestDto);
                }

                await SaveAsync();

                return PublisherMapper.ToDetailDto(publisher);
            }
            catch (InvalidOperationException ex)
            {
                // This catches the entity framework error about severed relationships
                throw new InvalidOperationException($"Cannot update publisher's books. Books must have a publisher assigned: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            // Add a generic catch to prevent crashes
            throw new ArgumentException($"Error updating publisher: {ex.Message}");
        }
    }

    private async Task ValidateRelatedEntitiesExistAsync(PublisherRequestDto requestDto)
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

    private async Task AssociateRelatedEntitiesAsync(Publisher publisher, PublisherRequestDto requestDto)
    {
        // Load and associate Books
        if (requestDto.BookIds.Any())
        {
            var books = await Context.Books
                .Where(b => requestDto.BookIds.Contains(b.Id))
                .ToListAsync();
            publisher.Books = books;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var hasBooks = await Context.Books.AnyAsync(b => b.PublisherId == id);
            if (hasBooks)
            {
                throw new InvalidOperationException("Cannot delete publisher with associated books. Reassign books to another publisher first.");
            }

            var publisher = await Context.Publishers.FirstOrDefaultAsync(p => p.Id == id);
            if (publisher == null)
            {
                return false;
            }

            Context.Publishers.Remove(publisher);
            await SaveAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException($"Database error while deleting publisher: {ex.Message}");
        }
        catch (Exception ex) when (!(ex is InvalidOperationException))
        {
            throw new ArgumentException($"Error deleting publisher: {ex.Message}");
        }
    }
}
