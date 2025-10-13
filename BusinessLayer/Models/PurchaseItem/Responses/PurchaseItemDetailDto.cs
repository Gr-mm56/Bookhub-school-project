using System.ComponentModel.DataAnnotations;
using BusinessLayer.Models.Book.Responses;

namespace BusinessLayer.Models.PurchaseItem.Responses;

public class PurchaseItemDetailDto : PurchaseItemDto
{
    [Required] public BookDto? Book { get; set; } = null;

    [Required] public CartDto? Cart { get; set; } = null;
}
