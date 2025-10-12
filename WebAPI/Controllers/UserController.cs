using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

[Route("controller")]
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
    public Task<IActionResult> UpdateUser(int id, [FromBody] User entity) => Update(id, entity);

    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteUser(int id) => Delete(id);
}
