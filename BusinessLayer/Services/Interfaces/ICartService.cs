using BusinessLayer.Models.Common;
using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Models.Cart.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface ICartService
{
    Task<PagedResultDto<CartDto>> GetCartsAsync(int limit = 20, int offset = 0);

    Task<CartDto?> GetCartByIdAsync(int id);

    Task<CartDto> CreateCartAsync(CartCreateDto requestDto);

    Task<CartDto?> UpdateCartAsync(int id, CartUpdateDto requestDto);

    Task<bool> DeleteCartAsync(int id);
}
