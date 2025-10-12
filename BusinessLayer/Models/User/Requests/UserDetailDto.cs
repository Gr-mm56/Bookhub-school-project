using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Models.WishlistItem.Requests;

namespace BusinessLayer.Models.User.Requests;

public class UserDetailDto
{
    public ICollection<CartDto>? Carts { get; set; } = new List<CartDto>();

    public ICollection<WishlistItemDto>? WishlistItems { get; set; } = new List<WishlistItemDto>();

    public ICollection<RatingDto>? Ratings { get; set; } = new List<RatingDto>();
}
