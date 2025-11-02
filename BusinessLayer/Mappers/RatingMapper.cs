using BusinessLayer.Models.Rating.Requests;
using BusinessLayer.Models.Rating.Responses;
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
            BookId = rating.BookId,
            CreatedAt = rating.CreatedAt,
            UpdatedAt = rating.UpdatedAt,
        };
    }

    public static RatingDetailDto ToDetailDto(Rating rating)
    {
        ArgumentNullException.ThrowIfNull(rating);

        return new RatingDetailDto
        {
            Id = rating.Id,
            Stars = rating.Stars,
            CreatedAt = rating.CreatedAt,
            UpdatedAt = rating.UpdatedAt,
            Book = BookMapper.ToDto(rating.Book),
            User = UserMapper.ToDto(rating.User)
        };
    }

    public static Rating CreateEntity(RatingRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(requestDto);

        return new Rating
        {
            Stars = requestDto.Stars,
            UserId = requestDto.UserId,
            BookId = requestDto.BookId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
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
        return ratings.Select(ToDto);
    }

    public static IEnumerable<RatingDetailDto> ToDetailDtoList(IEnumerable<Rating> ratings)
    {
        return ratings.Select(ToDetailDto);
    }
}