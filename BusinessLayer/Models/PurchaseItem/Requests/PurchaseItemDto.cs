using System.ComponentModel.DataAnnotations;


namespace BusinessLayer.Models.PurchaseItem.Requests;

public class PurchaseItemDto
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public int CartId { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Count must be non-negative.")]
    public int Count { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
