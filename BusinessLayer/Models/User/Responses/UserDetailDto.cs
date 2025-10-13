using BusinessLayer.Models.Cart.Responses;
using BusinessLayer.Models.WishlistItem.Requests;
using BusinessLayer.Models.WishlistItem.Responses;

namespace BusinessLayer.Models.User.Responses;

public class UserDetailDto : UserDto
{
    public ICollection<CartDto>? Carts { get; set; } = new List<CartDto>();

    public ICollection<WishlistItemDto>? WishlistItems { get; set; } = new List<WishlistItemDto>();

    public ICollection<RatingDto>? Ratings { get; set; } = new List<RatingDto>();

    public ImageDto? ProfilePhoto { get; set; } = null;
}
