using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Models.Cart.Responses;
using BusinessLayer.Models.Common;

namespace BusinessLayer.Services.Interfaces;

public interface ICartService : ICrudService<CartDto, CartDetailDto, CartCreateDto, CartUpdateDto>
{
    Task<PagedResultDto<CartDetailDto>> GetAllOrdersAsync(int limit = 20, int offset = 0);

    Task<CartDetailDto?> GetCartByUserIdAsync(int userId);

    Task<CartDto> CreateOrderAsync(OrderCreateDto orderCreateDto, int? cartId = null);

    Task<CartDto?> UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto);

    Task<List<CartDetailDto>> GetAllOrdersForUserAsync(int userId);
}
