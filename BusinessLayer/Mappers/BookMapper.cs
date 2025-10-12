using BusinessLayer.Models.Book.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mappers;

public static class BookMapper
{
    public static BookDto ToDto(Book book)
    {
        ArgumentNullException.ThrowIfNull(book);

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description ?? string.Empty,
            Price = book.Price
        };
    }

    public static IEnumerable<BookDto> ToDtoList(IEnumerable<Book> books)
    {
        return books.Select(ToDto);
    }
}
