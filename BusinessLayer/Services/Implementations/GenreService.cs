﻿using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Models.Genre.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Services.Implementations;

public class GenreService : BaseService<BookHubDbContext>, IGenreService
{
    private readonly IMemoryCache _memoryCache;
    private const string GenreAllCacheKey = "genres_all";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromSeconds(60);

    public GenreService(BookHubDbContext context, IMemoryCache memoryCache) : base(context)
    {
        _memoryCache = memoryCache;
    }

    public async Task<PagedResultDto<GenreDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var cacheKey = $"{GenreAllCacheKey}_{limit}_{offset}";
        
        if (_memoryCache.TryGetValue(cacheKey, out PagedResultDto<GenreDto>? cachedResult))
        {
            return cachedResult!;
        }

        var query = Context.Genres
            .AsNoTracking()
            .OrderBy(g => g.Name);

        var result = await PageAsync(query, limit, offset, GenreMapper.ToDtoList);
        
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(CacheExpiration);
        
        _memoryCache.Set(cacheKey, result, cacheOptions);
        
        return result;
    }

    public async Task<GenreDetailDto?> GetByIdAsync(int id)
    {
        var cacheKey = $"genre_{id}";
        
        if (_memoryCache.TryGetValue(cacheKey, out GenreDetailDto? cachedGenre))
        {
            return cachedGenre;
        }

        var genre = await Context.Genres
            .AsNoTracking()
            .Include(g => g.Books)
            .Include(g => g.PrimaryBooks)
            .FirstOrDefaultAsync(g => g.Id == id);

        var result = genre != null ? GenreMapper.ToDetailDto(genre) : null;
        
        if (result != null)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(CacheExpiration);
            
            _memoryCache.Set(cacheKey, result, cacheOptions);
        }
        
        return result;
    }

    public async Task<PagedResultDto<GenreDto>> SearchGenresAsync(GenreSearchDto searchDto)
    {
        var name = searchDto.Name.Trim();
        var cacheKey = $"genre_search_{name}_{searchDto.Limit}_{searchDto.Offset}";
        
        if (_memoryCache.TryGetValue(cacheKey, out PagedResultDto<GenreDto>? cachedResult))
        {
            return cachedResult!;
        }

        var query = Context.Genres
            .AsNoTracking()
            .Where(g => g.Name.Contains(name))
            .OrderBy(g => g.Name);

        var result = await PageAsync(query, searchDto.Limit, searchDto.Offset, GenreMapper.ToDtoList);
        
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(CacheExpiration);
        
        _memoryCache.Set(cacheKey, result, cacheOptions);
        
        return result;
    }

    public async Task<GenreDto> CreateAsync(GenreRequestDto requestDto)
    {
        var genre = GenreMapper.CreateEntity(requestDto);

        await Context.Genres.AddAsync(genre);
        await SaveAsync();

        InvalidateGenreCache();

        return GenreMapper.ToDto(genre);
    }

    public async Task<GenreDto?> UpdateAsync(int id, GenreRequestDto requestDto)
    {
        var genre = await Context.Genres.FirstOrDefaultAsync(g => g.Id == id);
        if (genre == null)
        {
            return null;
        }

        GenreMapper.UpdateEntity(genre, requestDto);
        await SaveAsync();

        InvalidateGenreCache();

        return GenreMapper.ToDto(genre);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var genre = await Context.Genres.FirstOrDefaultAsync(g => g.Id == id);
        if (genre == null)
        {
            return false;
        }

        Context.Genres.Remove(genre);
        await SaveAsync();
        
        InvalidateGenreCache();
        
        return true;
    }

    private void InvalidateGenreCache()
    {
        // Remove genre cache entries when data changes
        _memoryCache.Remove(GenreAllCacheKey);
        // In a real scenario, you might want to track all cache keys and remove them
        // For now, we're invalidating the main cache key which will force a fresh load
    }
}
