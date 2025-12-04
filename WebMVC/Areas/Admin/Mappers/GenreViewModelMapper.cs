using BusinessLayer.Models.Genre.Responses;
using WebMVC.Areas.Admin.Models.Book;
using WebMVC.Areas.Admin.Models.Genre;

namespace WebMVC.Areas.Admin.Mappers;

public static class GenreViewModelMapper
{
    public static GenresViewModel ToListViewModel(ICollection<GenreDto> genreDtos)
    {
        ArgumentNullException.ThrowIfNull(genreDtos);

        return new GenresViewModel
        {
            Genres = genreDtos
                .OrderBy(x => x.Id)
                .Select(ToListItemViewModel)
                .ToList()
        };
    }

    public static GenreListItemViewModel ToListItemViewModel(GenreDto genreDto)
    {
        ArgumentNullException.ThrowIfNull(genreDto);

        return new GenreListItemViewModel
        {
            Id = genreDto.Id,
            Name = genreDto.Name,
            CreatedAt = genreDto.CreatedAt,
            UpdatedAt = genreDto.UpdatedAt
        };
    }

    public static GenreCreateEditViewModel ToCreateEditViewModel(GenreDto genreDto)
    {
        ArgumentNullException.ThrowIfNull(genreDto);

        return new GenreCreateEditViewModel
        {
            Name = genreDto.Name
        };
    }

    public static GenreDeleteViewModel ToDeleteViewModel(GenreDetailDto genreDetailDto)
    {
        ArgumentNullException.ThrowIfNull(genreDetailDto);

        return new GenreDeleteViewModel
        {
            Id = genreDetailDto.Id,
            Name = genreDetailDto.Name,
            CreatedAt = genreDetailDto.CreatedAt,
            UpdatedAt = genreDetailDto.UpdatedAt,
            PrimaryBooks = genreDetailDto.PrimaryBooks
                .Select(b => new BookListItemBasicViewModel
                {
                    Id = b.Id,
                    Title = b.Title
                })
                .ToList(),
            AssociatedBooks = genreDetailDto.Books
                .Select(b => new BookListItemBasicViewModel
                {
                    Id = b.Id,
                    Title = b.Title
                })
                .ToList()
        };
    }
}

