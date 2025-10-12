using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("image")]
[ApiController]
public class ImageController : BaseController<Image>
{
    public ImageController(IRepository<Image> repository) : base(repository)
    {

    }

    [HttpGet]
    public Task<IActionResult> GetAllImages() => GetAll();

    [HttpGet("{id}")]
    public Task<IActionResult> GetImage(int id) => Get(id);

    [HttpPost]
    public Task<IActionResult> CreateImage([FromBody] Image entity) => Insert(entity);

    [HttpPut("{id}")]
    public Task<IActionResult> UpdateImage(int id, [FromBody] Image entity) => Update(id, entity);

    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteImage(int id) => Delete(id);
}
