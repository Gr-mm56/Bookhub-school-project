using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.Order;

public class OrderDeleteViewModel
{
    public int Id { get; set; }

    public double TotalValue { get; set; }

    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public bool PaymentStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int PurchaseItemsCount { get; set; }

}
