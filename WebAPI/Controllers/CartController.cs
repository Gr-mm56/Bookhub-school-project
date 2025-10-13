using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Models.Cart.Responses;
using WebAPI.Controllers;

namespace WebAPI.Controllers;


public class CartController
    : BaseController<CartDto, CartCreateDto, CartUpdateDto, ICartService>
{
    public CartController(ICartService service) : base(service)
    {
    }
}
