using BusinessLayer.Models.Common;
using BusinessLayer.Models.PurchaseItem.Requests;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Models.PurchaseItem.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PurchaseItemController(IPurchaseItemService purchaseItemService) : Controller
{
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllPurchaseItems([FromQuery] PagedRequestDto pagedRequest)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await purchaseItemService.GetPurchaseItemsAsync(pagedRequest.Limit, pagedRequest.Offset);

        if (result.Items.Any())
        {
            return Ok(result);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("details/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPurchaseItem(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        PurchaseItemDto? purchaseItem = await purchaseItemService.GetPurchaseItemByIdAsync(id);

        return purchaseItem == null ? NotFound() : Ok(purchaseItem);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePurchaseItem([FromBody] PurchaseItemCreateDto purchaseItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await purchaseItemService.CreatePurchaseItemAsync(purchaseItem);

        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] PurchaseItemUpdateDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var purchaseItem = await purchaseItemService.UpdatePurchaseItemAsync(id, requestDto);
        if (purchaseItem == null)
        {
            return NotFound();
        }

        return Ok(purchaseItem);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePurchaseItem(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        bool result = await purchaseItemService.DeletePurchaseItemAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
