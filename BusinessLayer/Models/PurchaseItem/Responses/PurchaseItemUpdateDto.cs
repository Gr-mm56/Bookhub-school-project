using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.PurchaseItem.Responses;

public class PurchaseItemUpdateDto
{
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Count must be non-negative.")]
    public int Count { get; set; }
}
