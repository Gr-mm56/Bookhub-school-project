using BusinessLayer.Models.Common;
using BusinessLayer.Models.PurchaseItem.Requests;
using BusinessLayer.Models.PurchaseItem.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IPurchaseItemService
    : ICrudService<PurchaseItemDto, PurchaseItemDetailDto, PurchaseItemCreateDto, PurchaseItemUpdateDto>
{
    Task<PagedResultDto<PurchaseItemDetailDto>> GetAllDetailsAsync(int limit = 20, int offset = 0);

    Task<bool> DeleteByItemIdAsync(int bookId, int cartId);
}
