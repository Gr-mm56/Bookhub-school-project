using System.ComponentModel.DataAnnotations;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Cart.Requests;

namespace BusinessLayer.Models.PurchaseItem.Requests;

public class PurchaseItemDetailDto : PurchaseItemDto
{
    [Required] public BookDto? Book { get; set; } = null;

    [Required] public CartDto? Cart { get; set; } = null;
}
