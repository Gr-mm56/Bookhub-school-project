using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Models.Cart.Responses;
using BusinessLayer.Models.Common;

namespace BusinessLayer.Services.Interfaces;

public interface ICartService : ICrudService<CartDto, CartDetailDto, CartCreateDto, CartUpdateDto>
{
    Task<PagedResultDto<CartDto>> GetAllOrdersAsync(int limit = 20, int offset = 0);

    Task<CartDto> CreateOrderAsync(OrderCreateDto orderCreateDto);

    Task<CartDto?> UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto);
}
