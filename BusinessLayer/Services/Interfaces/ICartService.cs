using BusinessLayer.Models.Common;
using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Models.Cart.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface ICartService
    : ICrudService<CartDto, CartCreateDto, CartUpdateDto>
{
    Task<PagedResultDto<CartDto>> GetAllAsync(int limit = 20, int offset = 0);

    Task<CartDto?> GetByIdAsync(int id);

    Task<CartDto> CreateAsync(CartCreateDto requestDto);

    Task<CartDto?> UpdateAsync(int id, CartUpdateDto requestDto);

    Task<bool> DeleteAsync(int id);
}
