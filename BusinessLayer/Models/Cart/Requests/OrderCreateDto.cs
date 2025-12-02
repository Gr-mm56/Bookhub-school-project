using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Enums;

namespace BusinessLayer.Models.Cart.Requests;

public class OrderCreateDto
{
    public int UserId { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "TotalValue must be non-negative.")]
    public required double TotalValue { get; set; }

    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public List<int> BookIds = [];

    [EnumDataType(typeof(PaymentStatusEnum))]
    public PaymentStatusEnum PaymentStatus { get; set; }
}
