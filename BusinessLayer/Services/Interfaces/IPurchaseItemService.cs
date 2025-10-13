using BusinessLayer.Models.Common;
using BusinessLayer.Models.PurchaseItem.Requests;
using BusinessLayer.Models.PurchaseItem.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IPurchaseItemService
{
    Task<PagedResultDto<PurchaseItemDto>> GetPurchaseItemsAsync(int limit = 20, int offset = 0);

    Task<PurchaseItemDto?> GetPurchaseItemByIdAsync(int id);

    Task<PurchaseItemDto> CreatePurchaseItemAsync(PurchaseItemCreateDto requestDto);

    Task<PurchaseItemDto?> UpdatePurchaseItemAsync(int id, PurchaseItemUpdateDto requestDto);

    Task<bool> DeletePurchaseItemAsync(int id);
}
