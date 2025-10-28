using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Models.Cart.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface ICartService : ICrudService<CartDto, CartDetailDto, CartCreateDto, CartUpdateDto>
{
}
