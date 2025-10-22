using BusinessLayer.Models.User.Requests;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Models.User.Responses;

namespace WebAPI.Controllers;

public class UserController : BaseController<UserDto, UserCreateDto, UserUpdateDto, IUserService>
{
    public UserController(IUserService service) : base(service)
    {
    }
}
