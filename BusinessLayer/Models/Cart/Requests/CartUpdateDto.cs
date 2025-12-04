using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Cart.Requests;

public class CartUpdateDto
{
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "TotalValue must be non-negative.")]
    public required double TotalValue { get; set; }

    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    [Required]
    public bool PaymentStatus { get; set; }
}
