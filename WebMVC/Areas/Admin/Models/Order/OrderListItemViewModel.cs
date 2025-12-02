using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Enums;

namespace WebMVC.Areas.Admin.Models.Order;

public class OrderListItemViewModel
{
    public int Id { get; set; }

    public double TotalValue { get; set; }

    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    [EnumDataType(typeof(PaymentStatusEnum))]
    public PaymentStatusEnum PaymentStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
