using BusinessLayer.Models.Common;
using BusinessLayer.Models.User.Requests;
using BusinessLayer.Models.User.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IUserService
{
    Task<PagedResultDto<UserCreateDto>> GetUsersAsync(int limit = 20, int offset = 0);
    Task<UserCreateDto?> GetUserByIdAsync(int id);
    Task<UserDetailDto?> GetUserWithBooksAsync(int id);
    Task<PagedResultDto<UserCreateDto>> SearchUsersAsync(UserSearchDto searchDto);
    Task<UserCreateDto> CreateUserAsync(UserRequestDto requestDto);
    Task<UserCreateDto?> UpdateUserAsync(int id, UserRequestDto requestDto);
    Task<bool> DeleteUserAsync(int id);
}
