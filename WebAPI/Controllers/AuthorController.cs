using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("author")]
[ApiController]
public class AuthorController : BaseController<Author>
{
    public AuthorController(IRepository<Author> repository) : base(repository)
    {

    }

    [HttpGet]
    public Task<IActionResult> GetAllAuthors() => GetAll();

    [HttpGet("{id}")]
    public Task<IActionResult> GetAuthor(int id) => Get(id);

    [HttpPost]
    public Task<IActionResult> CreateAuthor([FromBody] Author entity) => Insert(entity);

    [HttpPut("{id}")]
    public Task<IActionResult> UpdateAuthor(int id, [FromBody] Author entity) => Update(id, entity);

    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteAuthor(int id) => Delete(id);
}