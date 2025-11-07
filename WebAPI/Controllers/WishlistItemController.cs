using BusinessLayer.Models.Common;
using BusinessLayer.Models.WishlistItem.Requests;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Models.WishlistItem.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class WishlistItemController(IWishlistItemService wishlistItemService) : Controller
{
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllWishlistItems([FromQuery] PagedRequestDto pagedRequest)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await wishlistItemService.GetWishlistItemsAsync(pagedRequest.Limit, pagedRequest.Offset);

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
    public async Task<IActionResult> GetWishlistItem(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        WishlistItemDetailDto? wishlistItem = await wishlistItemService.GetWishlistItemByIdAsync(id);

        return wishlistItem == null ? NotFound() : Ok(wishlistItem);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateWishlistItem([FromBody] WishlistItemCreateDto wishlistItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await wishlistItemService.CreateWishlistItemAsync(wishlistItem);

        return Created();
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteWishlistItem(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        bool result = await wishlistItemService.DeleteWishlistItemAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
