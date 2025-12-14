using BusinessLayer.Models.User.Requests;
using BusinessLayer.Models.User.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interfaces;

public interface IUserService : ICrudService<UserDto, UserDetailDto, UserCreateDto, UserUpdateDto>
{
    Task<UserDto?> GetUserAddress(int userId);

    Task<bool> UpdateFromOrderAsync(int userId, UserOrderUpdateDto userDto);
}
