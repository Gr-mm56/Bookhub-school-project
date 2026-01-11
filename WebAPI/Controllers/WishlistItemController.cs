using BusinessLayer.Models.WishlistItem.Requests;
using BusinessLayer.Models.WishlistItem.Responses;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class WishlistItemController
    : BaseController<WishlistItemDetailDto, WishlistItemDetailDto, WishlistItemCreateDto, WishlistItemCreateDto, IWishlistItemService>
{
    public WishlistItemController(IWishlistItemService service) : base(service)
    {
    }

    [HttpPut("{id:int}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<IActionResult> Update(int id, WishlistItemCreateDto dto)
    {
        // 405 Not Allowed
        return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status405MethodNotAllowed));
    }
}
