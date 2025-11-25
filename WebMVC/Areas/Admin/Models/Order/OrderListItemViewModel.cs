namespace WebMVC.Areas.Admin.Models.Order;

public class OrderListItemViewModel
{
    public int Id { get; set; }
    public double TotalValue { get; set; }
    public int? OrderId { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public int PurchaseItemsCount { get; set; }
}
