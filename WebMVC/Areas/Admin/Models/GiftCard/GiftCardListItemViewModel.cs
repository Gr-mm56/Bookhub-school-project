namespace WebMVC.Areas.Admin.Models.GiftCard;

public class GiftCardListItemViewModel
{
    public int Id { get; set; }

    public double PriceReduction { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public int TotalCoupons { get; set; }

    public int UsedCoupons { get; set; }

    public int AvailableCoupons { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
