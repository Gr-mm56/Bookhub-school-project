using BusinessLayer.Models.User.Requests;
using BusinessLayer.Models.User.Responses;
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
            Name = user.Name
        };
    }

    public static UserDetailDto ToDetailDto(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new UserDetailDto
        {
            //TODO DETAIL COLUMNS
        };
    }

    public static User CreateDtoToEntity(UserCreateDto createDto)
    {
        ArgumentNullException.ThrowIfNull(createDto);

        return new User
        {
            //TODO COLUMNS
        };
    }

    public static void UpdateEntity(User user, UserUpdateDto updateDto)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(updateDto);

        //TODO COLUMNS
        user.Name = updateDto.Name;
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
