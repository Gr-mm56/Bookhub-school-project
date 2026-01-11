using BusinessLayer.Models.User.Requests;
using BusinessLayer.Models.User.Responses;
using BusinessLayer.Services.Interfaces;

namespace WebAPI.Controllers;

public class UserController : BaseController<UserDto, UserDetailDto, UserCreateDto, UserUpdateDto, IUserService>
{
    public UserController(IUserService service) : base(service)
    {
    }
}
