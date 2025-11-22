using BusinessLayer.Models.Book.Responses;
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
}

