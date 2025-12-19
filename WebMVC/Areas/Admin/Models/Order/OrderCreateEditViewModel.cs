using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebMVC.Areas.Admin.Models.Order;

public class OrderCreateEditViewModel
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Total value is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Total value must be non-negative")]
    public required double TotalValue { get; set; }

    [JsonIgnore]
    public double TotalValueRounded => Math.Round(TotalValue, 2);

    [DataType(DataType.DateTime)]
    public DateTime? OrderDate { get; set; }

    [Required]
    public int PaymentStatus { get; set; }

    [MinLength(1, ErrorMessage = "At least one book must be selected")]
    public List<int> BookIds { get; set; } = [];

    public string Country { get; set; } = "";

    public string City { get; set; } = "";

    public string Street { get; set; } = "";
}
