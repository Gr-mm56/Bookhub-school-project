using BusinessLayer.Models.Rating.Requests;
using BusinessLayer.Models.Rating.Responses;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Image.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mappers;

public static class RatingMapper
{
    public static RatingDto ToDto(Rating rating)
    {
        ArgumentNullException.ThrowIfNull(rating);

        return new RatingDto
        {
            Id = rating.Id,
            Stars = rating.Stars,
            UserId = rating.UserId,
            BookId = rating.BookId
        };
    }

    public static RatingDetailDto ToDetailDto(Rating rating)
    {
        ArgumentNullException.ThrowIfNull(rating);

        return new RatingDetailDto
        {
            Id = rating.Id,
            Stars = rating.Stars,
            Book = new BookDto
            {
                Id = rating.Book.Id,
                Title = rating.Book.Title,
                Description = rating.Book.Description,
                Price = rating.Book.Price,
                Image = rating.Book.Image != null ? new ImageDto
                {
                    Id = rating.Book.Image.Id,
                    FileUrl = rating.Book.Image.FileUrl
                } : null,
            }
            // User will be added when UserDto is ready
        };
    }

    public static Rating ToEntity(RatingRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(requestDto);

        return new Rating
        {
            Stars = requestDto.Stars,
            UserId = requestDto.UserId,
            BookId = requestDto.BookId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static void UpdateEntity(Rating rating, RatingRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(rating);
        ArgumentNullException.ThrowIfNull(requestDto);

        rating.Stars = requestDto.Stars;
        rating.UserId = requestDto.UserId;
        rating.BookId = requestDto.BookId;
        rating.UpdatedAt = DateTime.UtcNow;
    }

    public static IEnumerable<RatingDto> ToDtoList(IEnumerable<Rating> ratings)
    {
        return ratings?.Select(ToDto) ?? [];
    }

    public static IEnumerable<RatingDetailDto> ToDetailDtoList(IEnumerable<Rating> ratings)
    {
        return ratings?.Select(ToDetailDto) ?? [];
    }
}