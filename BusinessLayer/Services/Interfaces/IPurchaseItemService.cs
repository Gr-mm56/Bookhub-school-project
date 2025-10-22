using BusinessLayer.Models.Common;
using BusinessLayer.Models.PurchaseItem.Requests;
using BusinessLayer.Models.PurchaseItem.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IPurchaseItemService : ICrudService<PurchaseItemDto, PurchaseItemCreateDto, PurchaseItemUpdateDto>
{
    Task<PagedResultDto<PurchaseItemDto>> GetAllAsync(int limit = 20, int offset = 0);

    Task<PurchaseItemDto?> GetByIdAsync(int id);

    Task<PurchaseItemDto> CreateAsync(PurchaseItemCreateDto requestDto);

    Task<PurchaseItemDto?> UpdateAsync(int id, PurchaseItemUpdateDto requestDto);

    Task<bool> DeleteAsync(int id);
}
