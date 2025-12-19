using BusinessLayer.Models.Publisher.Requests;
using BusinessLayer.Models.Publisher.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mappers;

public static class PublisherMapper
{
    public static PublisherDto ToDto(Publisher publisher)
    {
        ArgumentNullException.ThrowIfNull(publisher);

        return new PublisherDto
        {
            Id = publisher.Id,
            Name = publisher.Name,
            Address = publisher.Address,
            ProfilePhoto = publisher.ProfilePhoto != null ? ImageMapper.ToDto(publisher.ProfilePhoto) : null,
            CreatedAt = publisher.CreatedAt,
            UpdatedAt = publisher.UpdatedAt
        };
    }

    public static PublisherBooksDto ToDetailDto(Publisher publisher)
    {
        ArgumentNullException.ThrowIfNull(publisher);

        return new PublisherBooksDto
        {
            Id = publisher.Id,
            Name = publisher.Name,
            Address = publisher.Address,
            ProfilePhoto = publisher.ProfilePhoto != null ? ImageMapper.ToDto(publisher.ProfilePhoto) : null,
            Books = publisher.Books.Select(BookMapper.ToDto).ToList() ?? [],
            CreatedAt = publisher.CreatedAt,
            UpdatedAt = publisher.UpdatedAt
        };
    }

    public static Publisher CreateEntity(PublisherRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(requestDto);

        return new Publisher
        {
            Name = requestDto.Name,
            Address = requestDto.Address,
            ProfilePhotoId = requestDto.ProfilePhotoId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Books = new List<Book>(),
        };
    }

    public static void UpdateEntity(Publisher publisher, PublisherRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(publisher);
        ArgumentNullException.ThrowIfNull(requestDto);

        publisher.Name = requestDto.Name;
        publisher.Address = requestDto.Address;
        publisher.ProfilePhotoId = requestDto.ProfilePhotoId;
        publisher.UpdatedAt = DateTime.UtcNow;
    }

    public static IEnumerable<PublisherDto> ToDtoList(IEnumerable<Publisher> publishers)
    {
        return publishers.Select(ToDto);
    }

    public static IEnumerable<PublisherBooksDto> ToDetailDtoList(IEnumerable<Publisher> publishers)
    {
        return publishers.Select(ToDetailDto);
    }
}