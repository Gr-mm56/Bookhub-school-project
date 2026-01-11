using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Models.Cart.Responses;
using BusinessLayer.Services.Interfaces;

namespace WebAPI.Controllers;

public class CartController : BaseController<CartDto, CartDetailDto, CartCreateDto, CartUpdateDto, ICartService>
{
    public CartController(ICartService service) : base(service)
    {
    }
}
