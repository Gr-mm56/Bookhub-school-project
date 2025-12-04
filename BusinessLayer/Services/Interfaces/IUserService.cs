using BusinessLayer.Models.User.Requests;
using BusinessLayer.Models.User.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IUserService : ICrudService<UserDto, UserDetailDto, UserCreateDto, UserUpdateDto>
{
}
