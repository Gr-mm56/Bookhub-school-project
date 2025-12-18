using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.GiftCard.Requests;

public class GiftCardCreateDto : GiftCardBaseDto
{
    [Required]
    [Range(1, 1000, ErrorMessage = "Number of coupons must be between 1 and 1000.")]
    public required int NumberOfCoupons { get; set; }
}