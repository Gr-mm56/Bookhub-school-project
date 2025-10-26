using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Genre.Responses;
using BusinessLayer.Models.Image.Responses;
using BusinessLayer.Models.Publisher.Responses;
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
            Image = book.Image != null
                ? new ImageDto
                {
                    Id = book.Image.Id,
                    FileUrl = book.Image.FileUrl
                }
                : null
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
            Image = book.Image != null
                ? new ImageDto
                {
                    Id = book.Image.Id,
                    FileUrl = book.Image.FileUrl
                }
                : null,
            Authors = book.Authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Surname = a.Surname
            }).ToList(),
            Genres = book.Genres.Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToList(),
            Publisher = book.Publisher != null 
                ? new PublisherDto
                {
                    Id = book.Publisher.Id,
                    Name = book.Publisher.Name
                } 
                : null
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