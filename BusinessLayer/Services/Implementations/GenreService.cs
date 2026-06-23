using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Models.Genre.Responses;
using BusinessLayer.Services.Extensions;
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
        var query = Context.Genres
            .AsNoTracking()
            .OrderBy(g => g.Name);

        return await PageAsync(query, limit, offset, GenreMapper.ToDtoList);

    }

    public async Task<GenreDetailDto?> GetByIdAsync(int id)
    {
        var cacheKey = $"genre_{id}";

        return await _memoryCache.GetOrCreateAsync(
            cacheKey,
            CacheExpiration,
            async () =>
            {
                var genre = await Context.Genres
                    .AsNoTracking()
                    .WithDetailIncludes()
                    .FirstOrDefaultAsync(g => g.Id == id);

                return genre != null ? GenreMapper.ToDetailDto(genre) : null;
            }
        );
    }

    public async Task<PagedResultDto<GenreDto>> SearchGenresAsync(GenreSearchDto searchDto)
    {
        var name = searchDto.Name.Trim();
        var cacheKey = $"genre_search_{name}_{searchDto.Limit}_{searchDto.Offset}";

        return await _memoryCache.GetOrCreateAsync(
            cacheKey,
            CacheExpiration,
            async () =>
            {
                var query = Context.Genres
                    .AsNoTracking()
                    .Where(g => g.Name.Contains(name))
                    .OrderBy(g => g.Name);

                return await PageAsync(query, searchDto.Limit, searchDto.Offset, GenreMapper.ToDtoList);
            }
        );
    }

    public async Task<GenreDto> CreateAsync(GenreRequestDto requestDto)
    {
        var genre = GenreMapper.CreateEntity(requestDto);

        await Context.Genres.AddAsync(genre);
        await SaveAsync();

        _memoryCache.Remove(GenreAllCacheKey);
        _memoryCache.Remove("genre_search_*");

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

        _memoryCache.Remove(GenreAllCacheKey);
        _memoryCache.Remove($"genre_{id}");
        _memoryCache.Remove("genre_search_*");

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

        _memoryCache.Remove(GenreAllCacheKey);
        _memoryCache.Remove($"genre_{id}");
        _memoryCache.Remove("genre_search_*");

        return true;
    }
}
