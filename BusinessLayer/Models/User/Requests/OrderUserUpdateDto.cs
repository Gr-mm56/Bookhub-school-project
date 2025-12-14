using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.User.Requests;

public class UserOrderUpdateDto
{
    [MaxLength(64)] public string Country { get; set; } = "";

    [MaxLength(64)] public string City { get; set; } = "";

    [MaxLength(64)] public string Street { get; set; } = "";
}
