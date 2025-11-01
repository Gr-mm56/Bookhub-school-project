using BusinessLayer.Models.Book.Requests;
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
            Description = book.Description,
            Price = book.Price,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt,
            Image = book.Image != null ? ImageMapper.ToDto(book.Image) : null
        };
    }

    public static BookDetailDto ToDetailDto(Book book)
    {
        ArgumentNullException.ThrowIfNull(book);

        return new BookDetailDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Price = book.Price,
            ISBN = book.ISBN,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt,
            Image = book.Image != null ? ImageMapper.ToDto(book.Image) : null,
            Authors = AuthorMapper.ToDtoList(book.Authors).ToList(),
            Genres = GenreMapper.ToDtoList(book.Genres).ToList(),
            Publisher = book.Publisher != null ? PublisherMapper.ToDto(book.Publisher) : null
        };
    }

    public static Book ToEntity(BookRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(requestDto);

        return new Book
        {
            Title = requestDto.Title,
            ISBN = requestDto.ISBN,
            Description = requestDto.Description,
            Price = requestDto.Price,
            ImageId = requestDto.ImageId,
            PublisherId = requestDto.PublisherId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Genres = new List<Genre>(),
            Authors = new List<Author>()
        };
    }

    public static void UpdateEntity(Book book, BookRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(book);
        ArgumentNullException.ThrowIfNull(requestDto);

        book.Title = requestDto.Title;
        book.ISBN = requestDto.ISBN;
        book.Description = requestDto.Description;
        book.Price = requestDto.Price;
        book.ImageId = requestDto.ImageId;
        book.UpdatedAt = DateTime.Now;
    }

    public static IEnumerable<BookDto> ToDtoList(IEnumerable<Book> books)
    {
        return books.Select(ToDto);
    }

    public static IEnumerable<BookDetailDto> ToDetailDtoList(IEnumerable<Book> books)
    {
        return books.Select(ToDetailDto);
    }
}