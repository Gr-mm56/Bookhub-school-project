using BusinessLayer.Models.Common;
using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Models.Genre.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IGenreService
{
    Task<PagedResultDto<GenreDto>> GetGenresAsync(int limit = 20, int offset = 0);
    Task<GenreDto?> GetGenreByIdAsync(int id);
    Task<GenreDetailDto?> GetGenreWithBooksAsync(int id);
    Task<PagedResultDto<GenreDto>> SearchGenresAsync(GenreSearchDto searchDto);
    Task<GenreDto> CreateGenreAsync(GenreRequestDto requestDto);
    Task<GenreDto?> UpdateGenreAsync(int id, GenreRequestDto requestDto);
    Task<bool> DeleteGenreAsync(int id);
}