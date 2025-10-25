using BusinessLayer.Models.Common;
using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Models.Genre.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IGenreService : ICrudService<GenreDto, GenreRequestDto, GenreRequestDto>
{
    Task<GenreDetailDto?> GetGenreWithBooksAsync(int id);
    Task<PagedResultDto<GenreDto>> SearchGenresAsync(GenreSearchDto searchDto);
}