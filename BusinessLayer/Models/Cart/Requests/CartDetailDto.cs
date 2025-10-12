using BusinessLayer.Models.PurchaseItem.Requests;
using BusinessLayer.Models.User.Requests;

namespace BusinessLayer.Models.Cart.Requests;

public class CartDetailDto : CartDto
{
    public UserDto? User { get; set; } = null;

    public ICollection<PurchaseItemDto>? PurchaseItems { get; set; } = new List<PurchaseItemDto>();
}
