using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.User.Responses;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.WishlistItem.Responses;

public class WishlistItemDetailDto : WishlistItemDto
{
    [Required]
    public required UserDto User { get; set; }

    [Required]
    public required BookDto Book { get; set; }
}
