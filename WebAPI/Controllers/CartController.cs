using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CartController : BaseController<Cart>
{
    public CartController(IRepository<Cart> repository) : base(repository)
    {

    }

    [HttpGet]
    public Task<IActionResult> GetAllCarts() => GetAll();

    [HttpGet("{id}")]
    public Task<IActionResult> GetCart(int id) => Get(id);

    [HttpPost]
    public Task<IActionResult> CreateCart([FromBody] Cart entity) => Insert(entity);

    [HttpPut("{id}")]
    public Task<IActionResult> UpdateCart(int id, [FromBody] Cart entity) => Update(id, entity);

    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteCart(int id) => Delete(id);
}
