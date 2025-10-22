using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Models.Cart.Responses;

namespace WebAPI.Controllers;

public class CartController : BaseController<CartDto, CartCreateDto, CartUpdateDto, ICartService>
{
    public CartController(ICartService service) : base(service)
    {
    }
}
