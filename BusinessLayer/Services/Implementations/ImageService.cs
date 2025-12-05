﻿using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Image.Requests;
using BusinessLayer.Models.Image.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Services.Implementations;

public class ImageService : BaseService<BookHubDbContext>, IImageService
{
    private readonly IMemoryCache _memoryCache;
    private const string ImageAllCacheKey = "images_all";
    private const string ImageDetailCacheKey = "images_detail";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromSeconds(10);

    public ImageService(BookHubDbContext dbContext, IMemoryCache memoryCache) : base(dbContext)
    {
        _memoryCache = memoryCache;
    }

    public async Task<PagedResultDto<ImageDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var cacheKey = $"{ImageAllCacheKey}_{limit}_{offset}";
        
        if (_memoryCache.TryGetValue(cacheKey, out PagedResultDto<ImageDto>? cachedResult))
        {
            return cachedResult!;
        }

        var query = Context.Images
            .AsNoTracking()
            .OrderBy(i => i.Id);

        var result = await PageAsync(query, limit, offset, ImageMapper.ToDtoList);
        
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(CacheExpiration);
        
        _memoryCache.Set(cacheKey, result, cacheOptions);
        
        return result;
    }

    public async Task<ImageDto?> GetByIdAsync(int id)
    {
        var cacheKey = $"{ImageDetailCacheKey}_{id}";
        
        if (_memoryCache.TryGetValue(cacheKey, out ImageDto? cachedImage))
        {
            return cachedImage;
        }

        var image = await Context.Images
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);

        var result = image != null ? ImageMapper.ToDto(image) : null;
        
        if (result != null)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(CacheExpiration);
            
            _memoryCache.Set(cacheKey, result, cacheOptions);
        }
        
        return result;
    }

    public async Task<ImageDto> CreateAsync(ImageRequestDto requestDto)
    {
        var image = ImageMapper.CreateEntity(requestDto);

        await Context.Images.AddAsync(image);
        await SaveAsync();

        var createdImage = await Context.Images
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.User)
            .FirstAsync(p => p.Id == image.Id);

        InvalidateImageCache();

        return ImageMapper.ToDto(createdImage);
    }

    public async Task<ImageDto> UpdateAsync(int id, ImageRequestDto requestDto)
    {
        var image = await Context.Images
            .FirstOrDefaultAsync(i => i.Id == id);

        if (image == null)
        {
            return null;
        }

        // Update properties
        ImageMapper.UpdateEntity(image, requestDto);

        await SaveAsync();

        InvalidateImageCache();

        return ImageMapper.ToDto(image);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var image = await Context.Images.FirstOrDefaultAsync(i => i.Id == id);
        if (image == null)
        {
            return false;
        }

        // Check for references before deleting
        var hasAuthorReferences = await Context.Authors.AnyAsync(a => a.ProfilePhotoId == id);
        var hasBookReferences = await Context.Books.AnyAsync(b => b.ImageId == id);
        var hasPublisherReferences = await Context.Publishers.AnyAsync(p => p.ProfilePhotoId == id);
        var hasUserReferences = await Context.Users.AnyAsync(u => u.ProfilePhotoId == id);

        if (hasAuthorReferences || hasBookReferences || hasPublisherReferences || hasUserReferences)
        {
            // Image is referenced by other entities and cannot be deleted
            return false;
        }

        Context.Images.Remove(image);
        await SaveAsync();
        
        InvalidateImageCache();
        
        return true;
    }

    private void InvalidateImageCache()
    {
        _memoryCache.Remove(ImageAllCacheKey);
    }
}
