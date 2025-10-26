using BusinessLayer.Models.Rating.Requests;
using BusinessLayer.Models.Rating.Responses;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Image.Responses;
using BusinessLayer.Models.User.Responses;
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
            },
            User = new UserDto
            {
                Id = rating.User.Id,
                City = rating.User.City,
                Country = rating.User.Country,
                CreatedAt = rating.User.CreatedAt,
                UpdatedAt = rating.User.UpdatedAt,
                Name = rating.User.Name,
                ProfilePhotoId = rating.User.ProfilePhotoId,
                Surname = rating.User.Surname,
                Street = rating.User.Street,
            }
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
        return ratings?.Select(ToDto) ?? [];
    }

    public static IEnumerable<RatingDetailDto> ToDetailDtoList(IEnumerable<Rating> ratings)
    {
        return ratings?.Select(ToDetailDto) ?? [];
    }
}