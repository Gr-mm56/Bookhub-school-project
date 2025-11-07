using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Models.Genre.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mappers;

public static class GenreMapper
{
    public static GenreDto ToDto(Genre genre)
    {
        ArgumentNullException.ThrowIfNull(genre);

        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name,
            CreatedAt = genre.CreatedAt,
            UpdatedAt = genre.UpdatedAt,
        };
    }

    public static GenreDetailDto ToDetailDto(Genre genre)
    {
        ArgumentNullException.ThrowIfNull(genre);

        return new GenreDetailDto
        {
            Id = genre.Id,
            Name = genre.Name,
            CreatedAt = genre.CreatedAt,
            UpdatedAt = genre.UpdatedAt,
            Books = genre.Books.Select(BookMapper.ToDto).ToList()
        };
    }

    public static Genre CreateEntity(GenreRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(requestDto);

        return new Genre
        {
            Name = requestDto.Name,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Books = new List<Book>(),
            
        };
    }

    public static void UpdateEntity(Genre genre, GenreRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(genre);
        ArgumentNullException.ThrowIfNull(requestDto);

        genre.Name = requestDto.Name;
        genre.UpdatedAt = DateTime.Now;
    }

    public static IEnumerable<GenreDto> ToDtoList(IEnumerable<Genre> genres)
    {
        return genres.Select(ToDto);
    }

    public static IEnumerable<GenreDetailDto> ToDetailDtoList(IEnumerable<Genre> genres)
    {
        return genres.Select(ToDetailDto);
    }
}
