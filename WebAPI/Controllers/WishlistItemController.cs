using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class WishlistItemController : BaseController<WishlistItem>
{
    public WishlistItemController(IRepository<WishlistItem> repository) : base(repository)
    {

    }

    [HttpGet]
    public Task<IActionResult> GetAllWishlistItems() => GetAll();

    [HttpGet("{id}")]
    public Task<IActionResult> GetWishlistItem(int id) => Get(id);

    [HttpPost]
    public Task<IActionResult> CreateWishlistItem([FromBody] WishlistItem entity) => Insert(entity);

    [HttpPut("{id}")]
    public Task<IActionResult> UpdateWishlistItem(int id, [FromBody] WishlistItem entity) => Update(id, entity);

    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteWishlistItem(int id) => Delete(id);
}
