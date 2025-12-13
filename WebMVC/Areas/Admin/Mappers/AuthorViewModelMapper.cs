using BusinessLayer.Models.Author.Requests;
using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Image.Responses;
using WebMVC.Areas.Admin.Models.Author;

namespace WebMVC.Areas.Admin.Mappers;

public static class AuthorViewModelMapper
{
    public static AuthorViewModel ToListViewModel(ICollection<AuthorBooksDto> authorDtos)
    {
        ArgumentNullException.ThrowIfNull(authorDtos);

        return new AuthorViewModel
        {
            Authors = authorDtos
                .OrderBy(x => x.Id)
                .Select(ToListItemViewModel)
                .ToList()
        };
    }

    public static AuthorListItemViewModel ToListItemViewModel(AuthorBooksDto authorDto)
    {
        ArgumentNullException.ThrowIfNull(authorDto);

        return new AuthorListItemViewModel
        {
            Id = authorDto.Id,
            Name = authorDto.Name,
            Surname = authorDto.Surname,
            CreatedAt = authorDto.CreatedAt,
            UpdatedAt = authorDto.UpdatedAt,
            BookCount = authorDto.Books?.Count ?? 0
        };
    }

    public static AuthorRequestDto ToRequestDto(AuthorCreateEditViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return new AuthorRequestDto
        {
            Name = viewModel.Name,
            Surname = viewModel.Surname,
            ProfilePhotoId = viewModel.ProfilePhotoId,
            BookIds = viewModel.BookIds
        };
    }

    public static AuthorCreateEditViewModel ToDtoToCreateEditViewModel(AuthorBooksDto authorDto)
    {
        ArgumentNullException.ThrowIfNull(authorDto);

        return new AuthorCreateEditViewModel
        {
            Name = authorDto.Name,
            Surname = authorDto.Surname,
            ProfilePhotoId = authorDto.ProfilePhoto?.Id,
            BookIds = authorDto.Books?.Select(b => b.Id).ToList() ?? []
        };
    }

    public static AuthorCreateEditViewModelWithOptions ToCreateEditViewModelWithOptions(
        AuthorCreateEditViewModel authorViewModel,
        ICollection<ImageDto> images,
        ICollection<BookDto> books)
    {
        ArgumentNullException.ThrowIfNull(authorViewModel);
        ArgumentNullException.ThrowIfNull(images);
        ArgumentNullException.ThrowIfNull(books);

        return new AuthorCreateEditViewModelWithOptions
        {
            Author = authorViewModel,
            Images = images
                .Select(i => new ImageOption { Id = i.Id, FileName = i.FileUrl })
                .OrderBy(x => x.FileName)
                .ToList(),
            Books = books
                .Select(b => new BookOption { Id = b.Id, Title = b.Title })
                .OrderBy(x => x.Title)
                .ToList()
        };
    }

    public static AuthorDeleteViewModel ToDeleteViewModel(AuthorBooksDto authorDto)
    {
        ArgumentNullException.ThrowIfNull(authorDto);

        return new AuthorDeleteViewModel
        {
            Id = authorDto.Id,
            Name = authorDto.Name,
            Surname = authorDto.Surname,
            CreatedAt = authorDto.CreatedAt,
            UpdatedAt = authorDto.UpdatedAt
        };
    }
}