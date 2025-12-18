namespace WebMVC.Areas.Admin.Models.GiftCard;

public class GiftCardDeleteViewModel
{
    public int Id { get; set; }
    public double PriceReduction { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public int TotalCoupons { get; set; }
    public int UsedCoupons { get; set; }
    public List<CouponInfoViewModel> Coupons { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CouponInfoViewModel
{
    public string Code { get; set; } = string.Empty;
    public bool IsUsed { get; set; }
    public int? UsedInOrderId { get; set; }
}