using BusinessLayer.Models.GiftCard.Requests;
using BusinessLayer.Models.GiftCard.Responses;
using BusinessLayer.Services.Interfaces;

namespace WebAPI.Controllers;

public class GiftCardController : BaseController<GiftCardCouponsDto, GiftCardCouponsDto, GiftCardCreateDto, GiftCardUpdateDto, IGiftCardService>
{
    public GiftCardController(IGiftCardService giftCardService) : base(giftCardService)
    {
    }
}