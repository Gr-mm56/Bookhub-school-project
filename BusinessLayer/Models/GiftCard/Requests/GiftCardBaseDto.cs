using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.GiftCard.Requests;

public abstract class GiftCardBaseDto
{
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "PriceReduction must be greater than zero.")]
    public required double PriceReduction { get; set; }

    [Required]
    public required DateTime ValidFrom { get; set; }

    [Required]
    public required DateTime ValidTo { get; set; }
}