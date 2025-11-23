using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Genre.Responses;
using BusinessLayer.Models.Image.Responses;
using BusinessLayer.Models.Publisher.Responses;
using WebMVC.Areas.Admin.Models.Book;

namespace WebMVC.Areas.Admin.Mappers;

public static class BookViewModelMapper
{
    public static BookViewModel ToListViewModel(ICollection<BookDto> bookDtos)
    {
        ArgumentNullException.ThrowIfNull(bookDtos);

        return new BookViewModel
        {
            Books = bookDtos
                .OrderBy(x => x.Id)
                .Select(ToListItemViewModel)
                .ToList()
        };
    }

    public static BookListItemViewModel ToListItemViewModel(BookDto bookDto)
    {
        ArgumentNullException.ThrowIfNull(bookDto);

        return new BookListItemViewModel
        {
            Id = bookDto.Id,
            Title = bookDto.Title,
            CreatedAt = bookDto.CreatedAt,
            UpdatedAt = bookDto.UpdatedAt,
            Price = bookDto.Price,
        };
    }

    public static BookRequestDto ToRequestDto(BookCreateEditViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return new BookRequestDto
        {
            Title = viewModel.Title,
            ISBN = viewModel.ISBN,
            Description = viewModel.Description,
            Price = viewModel.Price,
            PrimaryGenreId = viewModel.PrimaryGenreId,
            ImageId = viewModel.ImageId,
            PublisherId = viewModel.PublisherId,
            GenreIds = viewModel.GenreIds,
            AuthorIds = viewModel.AuthorIds
        };
    }
    // todo: publisher and Author overfetch! No need to fetch everything
    public static BookCreateEditViewModelWithOptions ToCreateEditViewModelWithOptions(
        BookCreateEditViewModel bookViewModel,
        ICollection<GenreDto> genres,
        ICollection<ImageDto> images,
        ICollection<PublisherBooksDto> publishers,
        ICollection<AuthorBooksDto> authors)
    {
        ArgumentNullException.ThrowIfNull(bookViewModel);
        ArgumentNullException.ThrowIfNull(genres);
        ArgumentNullException.ThrowIfNull(images);
        ArgumentNullException.ThrowIfNull(publishers);
        ArgumentNullException.ThrowIfNull(authors);

        return new BookCreateEditViewModelWithOptions
        {
            Book = bookViewModel,
            Genres = genres
                .Select(g => new GenreOption { Id = g.Id, Name = g.Name })
                .OrderBy(x => x.Name)
                .ToList(),
            Images = images
                .Select(i => new ImageOption { Id = i.Id, FileName = i.FileUrl })
                .OrderBy(x => x.FileName)
                .ToList(),
            Publishers = publishers
                .Select(p => new PublisherOption { Id = p.Id, Name = p.Name })
                .OrderBy(x => x.Name)
                .ToList(),
            Authors = authors
                .Select(a => new AuthorOption { Id = a.Id, Name = $"{a.Name} {a.Surname}" })
                .OrderBy(x => x.Name)
                .ToList()
        };
    }

    public static BookDeleteViewModel ToDeleteViewModel(BookDetailDto bookDetailDto)
    {
        ArgumentNullException.ThrowIfNull(bookDetailDto);

        return new BookDeleteViewModel
        {
            Id = bookDetailDto.Id,
            Title = bookDetailDto.Title,
            ISBN = bookDetailDto.ISBN,
            Price = bookDetailDto.Price,
            CreatedAt = bookDetailDto.CreatedAt,
            UpdatedAt = bookDetailDto.UpdatedAt
        };
    }
}
