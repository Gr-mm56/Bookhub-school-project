using BusinessLayer.Models.Cart.Responses;
using BusinessLayer.Models.Rating.Responses;
using BusinessLayer.Models.User.Requests;
using BusinessLayer.Models.User.Responses;
using BusinessLayer.Models.WishlistItem.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Street = user.Street,
            City = user.City,
            Country = user.Country,
            ProfilePhotoId = user.ProfilePhotoId,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public static UserDetailDto ToDetailDto(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new UserDetailDto
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Street = user.Street,
            City = user.City,
            Country = user.Country,
            ProfilePhotoId = user.ProfilePhotoId,
            Carts = user.Carts?.Select(CartMapper.ToDto).ToList() ?? new List<CartDto>(),
            Ratings = user.Ratings?.Select(RatingMapper.ToDto).ToList() ?? new List<RatingDto>(),
            WishlistItems = user.WishlistItems?.Select(WishlistItemMapper.ToDto).ToList() ?? new List<WishlistItemDto>(),
            ProfilePhoto = ImageMapper.ToDto(user.ProfilePhoto ?? new Image()),
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public static User CreateDtoToEntity(UserCreateDto createDto)
    {
        ArgumentNullException.ThrowIfNull(createDto);

        return new User
        {
            Name = createDto.Name,
            Surname = createDto.Surname,
            Street = createDto.Street,
            City = createDto.City,
            Country = createDto.Country,
            ProfilePhotoId = createDto.ProfilePhotoId,
            Carts = new List<Cart>(),
            Ratings = new List<Rating>(),
            WishlistItems = new List<WishlistItem>(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public static void UpdateEntity(User user, UserUpdateDto updateDto)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(updateDto);

        user.Name = updateDto.Name;
        user.Surname = updateDto.Surname;
        user.Street = updateDto.Street;
        user.City = updateDto.City;
        user.Country = updateDto.Country;
        user.ProfilePhotoId = updateDto.ProfilePhotoId;
        user.UpdatedAt = DateTime.Now;
    }

    public static IEnumerable<UserDto> ToDtoList(IEnumerable<User> users)
    {
        return users.Select(ToDto);
    }

    public static IEnumerable<UserDetailDto> ToDetailDtoList(IEnumerable<User> users)
    {
        return users.Select(ToDetailDto);
    }
}
