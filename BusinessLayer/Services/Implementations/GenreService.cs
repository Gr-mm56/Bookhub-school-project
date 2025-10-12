using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Models.Genre.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly BookHubDbContext _context;

    public GenreService(BookHubDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResultDto<GenreDto>> GetGenresAsync(int limit = 20, int offset = 0)
    {
        var totalCount = await _context.Genres.CountAsync();
        var genres = await _context.Genres
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return new PagedResultDto<GenreDto>
        {
            Total = totalCount,
            Items = GenreMapper.ToDtoList(genres),
            Offset = offset,
            Limit = limit
        };
    }

    public async Task<GenreDto?> GetGenreByIdAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        return genre != null ? GenreMapper.ToDto(genre) : null;
    }

    public async Task<GenreDetailDto?> GetGenreWithBooksAsync(int id)
    {
        var genre = await _context.Genres
            .Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Id == id);
        
        return genre != null ? GenreMapper.ToDetailDto(genre) : null;
    }

    public async Task<PagedResultDto<GenreDto>> SearchGenresAsync(GenreSearchDto searchDto)
    {
        var query = _context.Genres.AsQueryable();

        if (!string.IsNullOrEmpty(searchDto.Name))
        {
            query = query.Where(g => g.Name.Contains(searchDto.Name));
        }

        var totalCount = await query.CountAsync();
        var genres = await query
            .Skip(searchDto.Offset)
            .Take(searchDto.Limit)
            .ToListAsync();

        return new PagedResultDto<GenreDto>
        {
            Total = totalCount,
            Items = GenreMapper.ToDtoList(genres),
            Offset = searchDto.Offset,
            Limit = searchDto.Limit
        };
    }

    public async Task<GenreDto> CreateGenreAsync(GenreRequestDto requestDto)
    {
        var genre = GenreMapper.ToEntity(requestDto);
        
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
        
        return GenreMapper.ToDto(genre);
    }

    public async Task<GenreDto?> UpdateGenreAsync(int id, GenreRequestDto requestDto)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
            return null;

        GenreMapper.UpdateEntity(genre, requestDto);
        await _context.SaveChangesAsync();
        
        return GenreMapper.ToDto(genre);
    }

    public async Task<bool> DeleteGenreAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
            return false;

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return true;
    }
}