using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PurchaseItemController : BaseController<PurchaseItem>
{
    public PurchaseItemController(IRepository<PurchaseItem> repository) : base(repository)
    {

    }

    [HttpGet]
    public Task<IActionResult> GetAllPurchaseItems() => GetAll();

    [HttpGet("{id}")]
    public Task<IActionResult> GetPurchaseItem(int id) => Get(id);

    [HttpPost]
    public Task<IActionResult> CreatePurchaseItem([FromBody] PurchaseItem entity) => Insert(entity);

    [HttpPut("{id}")]
    public Task<IActionResult> UpdatePurchaseItem(int id, [FromBody] PurchaseItem entity) => Update(id, entity);

    [HttpDelete("{id}")]
    public Task<IActionResult> DeletePurchaseItem(int id) => Delete(id);
}
