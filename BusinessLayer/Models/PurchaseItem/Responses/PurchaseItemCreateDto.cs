using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.PurchaseItem.Responses;

public class PurchaseItemCreateDto
{
    public int BookId { get; set; }

    public int CartId { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Count must be non-negative.")]
    public int Count { get; set; }
}
