using BusinessLayer.Models.PurchaseItem.Requests;
using BusinessLayer.Models.PurchaseItem.Responses;
using BusinessLayer.Services.Interfaces;

namespace WebAPI.Controllers;

public class PurchaseItemController
    : BaseController<PurchaseItemDto, PurchaseItemDetailDto, PurchaseItemCreateDto, PurchaseItemUpdateDto, IPurchaseItemService>
{
    public PurchaseItemController(IPurchaseItemService service) : base(service)
    {
    }
}
