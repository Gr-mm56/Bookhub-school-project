using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Cart.Responses;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.PurchaseItem.Responses;

public class PurchaseItemDetailDto : PurchaseItemDto
{
    [Required]
    public required BookDto Book { get; set; }

    [Required]
    public required CartDto Cart { get; set; }
}
