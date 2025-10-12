using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Cart.Responses;

public class CartCreateDto
{
    public int UserId { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "TotalValue must be non-negative.")]
    public double TotalValue { get; set; }

    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }
}
