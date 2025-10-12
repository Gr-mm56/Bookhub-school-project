using System.ComponentModel.DataAnnotations;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.User.Requests;

namespace BusinessLayer.Models.WishlistItem.Requests;

public class WishlistItemDetailDto
{
    [Required] public UserDto? User { get; set; } = null;

    [Required] public BookDto? Book { get; set; } = null;
}
