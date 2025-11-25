using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.Order;

public class OrderCreateEditViewModel
{
    [Required(ErrorMessage = "User is required")]
    public required int UserId { get; set; }

    [Required(ErrorMessage = "Total value is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Total value must be non-negative")]
    public required double TotalValue { get; set; }

    public int? OrderId { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? OrderDate { get; set; }

    [MinLength(1, ErrorMessage = "At least one purchase item must be selected")]
    public List<int> PurchaseItemIds { get; set; } = [];
}
