using BusinessLayer.Models.Common;
using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Models.Cart.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CartController(ICartService cartService) : Controller
{
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllCarts([FromQuery] PagedRequestDto pagedRequest)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await cartService.GetCartsAsync(pagedRequest.Limit, pagedRequest.Offset);

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
    public async Task<IActionResult> GetCart(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        CartDto? cart = await cartService.GetCartByIdAsync(id);

        return cart == null ? NotFound() : Ok(cart);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCart([FromBody] CartCreateDto cart)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await cartService.CreateCartAsync(cart);

        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] CartUpdateDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cart = await cartService.UpdateCartAsync(id, requestDto);
        if (cart == null)
        {
            return NotFound();
        }

        return Ok(cart);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCart(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        bool result = await cartService.DeleteCartAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
