namespace WebMVC.Areas.Admin.Models.Order;

public class OrderListItemViewModel
{
    public int Id { get; set; }

    public double TotalValue { get; set; }

    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int PaymentStatus { get; set; }

    public string? AppliedGiftCardCode { get; set; }

    public double GiftCardDiscount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}
