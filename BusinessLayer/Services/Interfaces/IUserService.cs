using BusinessLayer.Models.Common;
using BusinessLayer.Models.User.Requests;
using BusinessLayer.Models.User.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IUserService
{
    Task<PagedResultDto<UserDto>> GetUsersAsync(int limit = 20, int offset = 0);

    Task<UserDto?> GetUserByIdAsync(int id);

    Task<UserDto> CreateUserAsync(UserCreateDto requestDto);

    Task<UserDto?> UpdateUserAsync(int id, UserUpdateDto requestDto);

    Task<bool> DeleteUserAsync(int id);
}
