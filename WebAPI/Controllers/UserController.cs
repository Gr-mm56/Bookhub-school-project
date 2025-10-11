using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

[Route("users")]
[ApiController]
public class UserController : BaseController<User>
{
    public UserController(IRepository<User> repository) : base(repository)
    {

    }

    [HttpGet]
    public Task<IActionResult> GetAllUsers() => GetAll();

    [HttpGet("{id}")]
    public Task<IActionResult> GetUser(int id) => Get(id);

    [HttpPost]
    public Task<IActionResult> CreateUser([FromBody] User entity) => Insert(entity);

    [HttpPut("{id}")]
    public Task<IActionResult> UpdateUser([FromBody] User entity) => Update(entity);

    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteUser(int id) => Delete(id);
}
