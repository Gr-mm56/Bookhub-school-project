using BusinessLayer.Models.PurchaseItem.Requests;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Models.PurchaseItem.Responses;

namespace WebAPI.Controllers;

public class PurchaseItemController
    : BaseController<PurchaseItemDto,PurchaseItemDetailDto, PurchaseItemCreateDto, PurchaseItemUpdateDto, IPurchaseItemService>
{
    public PurchaseItemController(IPurchaseItemService service) : base(service)
    {
    }
}
