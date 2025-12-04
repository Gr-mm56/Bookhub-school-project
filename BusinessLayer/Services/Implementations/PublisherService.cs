﻿using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Publisher.Requests;
using BusinessLayer.Models.Publisher.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Services.Implementations;

public class PublisherService : BaseService<BookHubDbContext>, IPublisherService
{
    private readonly IMemoryCache _memoryCache;
    private const string PublisherAllCacheKey = "publishers_all";
    private const string PublisherDetailCacheKey = "publishers_detail";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromSeconds(60);

    public PublisherService(BookHubDbContext dbContext, IMemoryCache memoryCache) : base(dbContext)
    {
        _memoryCache = memoryCache;
    }

    public async Task<PagedResultDto<PublisherBooksDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var cacheKey = $"{PublisherAllCacheKey}_{limit}_{offset}";
        
        if (_memoryCache.TryGetValue(cacheKey, out PagedResultDto<PublisherBooksDto>? cachedResult))
        {
            return cachedResult!;
        }

        var query = Context.Publishers
            .AsNoTracking()
            .Include(p => p.ProfilePhoto)
            .Include(b => b.Books)
                .ThenInclude(i => i.Image)
            .OrderBy(p => p.Name);

        var result = await PageAsync<Publisher, PublisherBooksDto>(
            query, 
            limit, 
            offset, 
            publishers => PublisherMapper.ToDetailDtoList(publishers)
        );
        
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(CacheExpiration);
        
        _memoryCache.Set(cacheKey, result, cacheOptions);
        
        return result;
    }

    public async Task<PublisherBooksDto?> GetByIdAsync(int id)
    {
        var cacheKey = $"{PublisherDetailCacheKey}_{id}";
        
        if (_memoryCache.TryGetValue(cacheKey, out PublisherBooksDto? cachedPublisher))
        {
            return cachedPublisher;
        }

        var publisher = await Context.Publishers
            .AsNoTracking()
            .Include(p => p.ProfilePhoto)
            .Include(b => b.Books)
                .ThenInclude(i => i.Image)
            .FirstOrDefaultAsync(p => p.Id == id);

        var result = publisher != null ? PublisherMapper.ToDetailDto(publisher) : null;
        
        if (result != null)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(CacheExpiration);
            
            _memoryCache.Set(cacheKey, result, cacheOptions);
        }
        
        return result;
    }

    public async Task<PublisherBooksDto> CreateAsync(PublisherRequestDto requestDto)
    {
        await ValidateRelatedEntitiesExistAsync(requestDto);

        var publisher = PublisherMapper.CreateEntity(requestDto);

        if (requestDto.ProfilePhotoId <= 0)
        {
            publisher.ProfilePhotoId = null;
        }

        await ExtendBooksCollectionAsync(publisher, requestDto);

        await Context.Publishers.AddAsync(publisher);
        await SaveAsync();

        // Reload with all related data
        var createdPublisher = await Context.Publishers
            .Include(p => p.ProfilePhoto)
            .Include(b => b.Books)
                .ThenInclude(i => i.Image)
            .FirstAsync(p => p.Id == publisher.Id);

        InvalidatePublisherCache();

        return PublisherMapper.ToDetailDto(createdPublisher);
    }

    public async Task<PublisherBooksDto?> UpdateAsync(int id, PublisherRequestDto requestDto)
    {
        var publisher = await Context.Publishers
            .Include(p => p.ProfilePhoto)
            .Include(b => b.Books)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (publisher == null)
        {
            return null;
        }

        await ValidateRelatedEntitiesExistAsync(requestDto);

        PublisherMapper.UpdateEntity(publisher, requestDto);

        if (requestDto.ProfilePhotoId <= 0)
        {
            publisher.ProfilePhotoId = null;
        }

        await ExtendBooksCollectionAsync(publisher, requestDto);

        await SaveAsync();

        // Reload with updated data
        var updatedPublisher = await Context.Publishers
            .Include(p => p.ProfilePhoto)
            .Include(b => b.Books)
                .ThenInclude(i => i.Image)
            .FirstAsync(p => p.Id == id);

        InvalidatePublisherCache();

        return PublisherMapper.ToDetailDto(updatedPublisher);
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

    private async Task ExtendBooksCollectionAsync(Publisher publisher, PublisherRequestDto requestDto)
    {
        if (requestDto.BookIds.Any())
        {
            var currentBookIds = publisher.Books.Select(b => b.Id).ToList();
            
            var newBookIds = requestDto.BookIds.Except(currentBookIds).ToList();
            
            if (newBookIds.Any())
            {
                var newBooks = await Context.Books
                    .Where(b => newBookIds.Contains(b.Id))
                    .ToListAsync();
                
                foreach (var book in newBooks)
                {
                    publisher.Books.Add(book);
                }
            }
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var publisher = await Context.Publishers
            .Include(p => p.Books)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (publisher == null)
        {
            return false;
        }

        if (publisher.Books.Any())
        {
            throw new InvalidOperationException("Cannot delete publisher who has associated books. Reassign books to another publisher first.");
        }

        Context.Publishers.Remove(publisher);
        await SaveAsync();
        
        InvalidatePublisherCache();
        
        return true;
    }

    private void InvalidatePublisherCache()
    {
        _memoryCache.Remove(PublisherAllCacheKey);
    }
}
