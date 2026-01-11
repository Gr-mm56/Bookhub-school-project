using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.GiftCard;

public class GiftCardCreateEditViewModel
{
    [Required(ErrorMessage = "Price reduction is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price reduction must be greater than zero")]
    [Display(Name = "Price Reduction ($)")]
    public double PriceReduction { get; set; }

    [Required(ErrorMessage = "Valid from date is required")]
    [Display(Name = "Valid From")]
    [DataType(DataType.Date)]
    public DateTime ValidFrom { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Valid to date is required")]
    [Display(Name = "Valid To")]
    [DataType(DataType.Date)]
    public DateTime ValidTo { get; set; } = DateTime.Now.AddMonths(3);

    [Required(ErrorMessage = "Number of coupons is required")]
    [Range(1, 1000, ErrorMessage = "Number of coupons must be between 1 and 1000")]
    [Display(Name = "Number of Coupon Codes")]
    public int NumberOfCoupons { get; set; } = 10;

    // For edit - adding more coupons
    [Range(0, 1000, ErrorMessage = "Additional coupons must be between 0 and 1000")]
    [Display(Name = "Additional Coupons to Generate")]
    public int AdditionalCoupons { get; set; } = 0;
}