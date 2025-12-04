using System.ComponentModel.DataAnnotations;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Cart.Responses;

namespace BusinessLayer.Models.PurchaseItem.Responses;

public class PurchaseItemDetailDto : PurchaseItemDto
{
    [Required]
    public required BookDto Book { get; set; }

    [Required]
    public required CartDto Cart { get; set; }
}
