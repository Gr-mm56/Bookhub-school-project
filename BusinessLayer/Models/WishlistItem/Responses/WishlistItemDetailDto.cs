using System.ComponentModel.DataAnnotations;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.User.Responses;

namespace BusinessLayer.Models.WishlistItem.Responses;

public class WishlistItemDetailDto : WishlistItemDto
{
    [Required]
    public required UserDto? User { get; set; } = null;

    [Required]
    public required BookDto? Book { get; set; } = null;
}
