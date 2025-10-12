using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("publisher")]
[ApiController]
public class PublisherController : BaseController<Publisher>
{
    public PublisherController(IRepository<Publisher> repository) : base(repository)
    {

    }

    [HttpGet]
    public Task<IActionResult> GetAllPublishers() => GetAll();

    [HttpGet("{id}")]
    public Task<IActionResult> GetPublisher(int id) => Get(id);

    [HttpPost]
    public Task<IActionResult> CreatePublisher([FromBody] Publisher entity) => Insert(entity);

    [HttpPut("{id}")]
    public Task<IActionResult> UpdatePublisher(int id, [FromBody] Publisher entity) => Update(id, entity);

    [HttpDelete("{id}")]
    public Task<IActionResult> DeletePublisher(int id) => Delete(id);
}
