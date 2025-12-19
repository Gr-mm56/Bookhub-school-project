using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Image.Responses;
using BusinessLayer.Models.Publisher.Requests;
using BusinessLayer.Models.Publisher.Responses;
using WebMVC.Areas.Admin.Models.Book;
using WebMVC.Areas.Admin.Models.Image;
using WebMVC.Areas.Admin.Models.Publisher;

namespace WebMVC.Areas.Admin.Mappers;

public static class PublisherViewModelMapper
{
    public static PublisherViewModel ToListViewModel(ICollection<PublisherBooksDto> publisherDtos)
    {
        ArgumentNullException.ThrowIfNull(publisherDtos);

        return new PublisherViewModel
        {
            Publishers = publisherDtos
                .OrderBy(x => x.Id)
                .Select(ToListItemViewModel)
                .ToList()
        };
    }

    public static PublisherListItemViewModel ToListItemViewModel(PublisherBooksDto publisherDto)
    {
        ArgumentNullException.ThrowIfNull(publisherDto);

        return new PublisherListItemViewModel
        {
            Id = publisherDto.Id,
            Name = publisherDto.Name,
            Address = publisherDto.Address,
            CreatedAt = publisherDto.CreatedAt,
            UpdatedAt = publisherDto.UpdatedAt,
            BookCount = publisherDto.Books?.Count ?? 0
        };
    }

    public static PublisherRequestDto ToRequestDto(PublisherCreateEditViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return new PublisherRequestDto
        {
            Name = viewModel.Name,
            Address = viewModel.Address,
            ProfilePhotoId = viewModel.ProfilePhotoId,
            BookIds = viewModel.BookIds
        };
    }

    public static PublisherCreateEditViewModel ToDtoToCreateEditViewModel(PublisherBooksDto publisherDto)
    {
        ArgumentNullException.ThrowIfNull(publisherDto);

        return new PublisherCreateEditViewModel
        {
            Name = publisherDto.Name,
            Address = publisherDto.Address,
            ProfilePhotoId = publisherDto.ProfilePhoto?.Id,
            BookIds = publisherDto.Books?.Select(b => b.Id).ToList() ?? []
        };
    }

    public static PublisherCreateEditViewModelWithOptions ToCreateEditViewModelWithOptions(
        PublisherCreateEditViewModel publisherViewModel,
        ICollection<ImageDto> images,
        ICollection<BookDto> books)
    {
        ArgumentNullException.ThrowIfNull(publisherViewModel);
        ArgumentNullException.ThrowIfNull(images);
        ArgumentNullException.ThrowIfNull(books);

        return new PublisherCreateEditViewModelWithOptions
        {
            Publisher = publisherViewModel,
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

    public static PublisherDeleteViewModel ToDeleteViewModel(PublisherBooksDto publisherDto)
    {
        ArgumentNullException.ThrowIfNull(publisherDto);

        return new PublisherDeleteViewModel
        {
            Id = publisherDto.Id,
            Name = publisherDto.Name,
            Address = publisherDto.Address,
            CreatedAt = publisherDto.CreatedAt,
            UpdatedAt = publisherDto.UpdatedAt
        };
    }
}