using BusinessLayer.Models.Common;
using BusinessLayer.Models.User.Requests;
using BusinessLayer.Models.User.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IUserService : ICrudService<UserDto, UserCreateDto, UserUpdateDto>
{
    Task<PagedResultDto<UserDto>> GetAllAsync(int limit = 20, int offset = 0);

    Task<UserDto?> GetByIdAsync(int id);

    Task<UserDto> CreateAsync(UserCreateDto requestDto);

    Task<UserDto?> UpdateAsync(int id, UserUpdateDto requestDto);

    Task<bool> DeleteAsync(int id);
}
