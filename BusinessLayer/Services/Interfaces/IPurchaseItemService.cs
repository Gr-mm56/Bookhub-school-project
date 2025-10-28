using BusinessLayer.Models.PurchaseItem.Requests;
using BusinessLayer.Models.PurchaseItem.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IPurchaseItemService
    : ICrudService<PurchaseItemDto, PurchaseItemDetailDto, PurchaseItemCreateDto, PurchaseItemUpdateDto>
{
}
