using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Models.Genre.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class GenreService : BaseService<BookHubDbContext>, IGenreService
{
    public GenreService(BookHubDbContext context) : base(context)
    {
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
        var genre = await Context.Genres
            .AsNoTracking()
            .Include(g => g.Books)
            .Include(g => g.PrimaryBooks)
            .FirstOrDefaultAsync(g => g.Id == id);

        return genre != null ? GenreMapper.ToDetailDto(genre) : null;
    }

    public async Task<PagedResultDto<GenreDto>> SearchGenresAsync(GenreSearchDto searchDto)
    {
        var name = searchDto.Name.Trim();
        var query = Context.Genres
            .AsNoTracking()
            .Where(g => g.Name.Contains(name))
            .OrderBy(g => g.Name);

        return await PageAsync(query, searchDto.Limit, searchDto.Offset, GenreMapper.ToDtoList);
    }

    public async Task<GenreDto> CreateAsync(GenreRequestDto requestDto)
    {
        var genre = GenreMapper.CreateEntity(requestDto);

        await Context.Genres.AddAsync(genre);
        await SaveAsync();

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
        return true;
    }
}
