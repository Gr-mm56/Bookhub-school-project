using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.GiftCard.Requests;

public class GiftCardUpdateDto : GiftCardBaseDto
{
    [Range(0, 1000, ErrorMessage = "Number of additional coupons must be between 0 and 1000.")]
    public int AdditionalCoupons { get; set; } = 0;
}