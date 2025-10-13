using BusinessLayer.Models.PurchaseItem.Responses;
using BusinessLayer.Models.User.Requests;

namespace BusinessLayer.Models.Cart.Responses;

public class CartDetailDto : CartDto
{
    public UserDto? User { get; set; } = null;

    public ICollection<PurchaseItemDto>? PurchaseItems { get; set; } = new List<PurchaseItemDto>();
}
