using BusinessLayer.Models.Author.Requests;
using BusinessLayer.Models.Author.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mappers;

public static class AuthorMapper
{
    public static AuthorDto ToDto(Author author)
    {
        ArgumentNullException.ThrowIfNull(author);

        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            Surname = author.Surname,
            ProfilePhoto = author.ProfilePhoto != null ? ImageMapper.ToDto(author.ProfilePhoto) : null,
            CreatedAt = author.CreatedAt,
            UpdatedAt = author.UpdatedAt
        };
    }

    public static AuthorBooksDto ToDetailDto(Author author)
    {
        ArgumentNullException.ThrowIfNull(author);

        return new AuthorBooksDto
        {
            Id = author.Id,
            Name = author.Name,
            Surname = author.Surname,
            ProfilePhoto = author.ProfilePhoto != null ? ImageMapper.ToDto(author.ProfilePhoto) : null,
            Books = author.Books.Select(BookMapper.ToDto).ToList() ?? [],
            CreatedAt = author.CreatedAt,
            UpdatedAt = author.UpdatedAt
        };
    }

    public static Author CreateEntity(AuthorRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(requestDto);

        return new Author
        {
            Name = requestDto.Name,
            Surname = requestDto.Surname,
            ProfilePhotoId = requestDto.ProfilePhotoId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Books = new List<Book>(),
        };
    }

    public static void UpdateEntity(Author author, AuthorRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(author);
        ArgumentNullException.ThrowIfNull(requestDto);

        author.Name = requestDto.Name;
        author.Surname = requestDto.Surname;
        author.ProfilePhotoId = requestDto.ProfilePhotoId;
        author.UpdatedAt = DateTime.UtcNow;
    }

    public static IEnumerable<AuthorDto> ToDtoList(IEnumerable<Author> author)
    {
        return author.Select(ToDto);
    }

    public static IEnumerable<AuthorBooksDto> ToDetailDtoList(IEnumerable<Author> authors)
    {
        return authors.Select(ToDetailDto);
    }
}