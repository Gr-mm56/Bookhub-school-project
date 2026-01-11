using BusinessLayer.Models.GiftCard.Requests;
using BusinessLayer.Models.GiftCard.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IGiftCardService : ICrudService<GiftCardCouponsDto, GiftCardCouponsDto, GiftCardCreateDto, GiftCardUpdateDto>
{
}