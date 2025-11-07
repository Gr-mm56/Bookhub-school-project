using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.User.Responses;

public class UserDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Surname { get; set; }

    [MaxLength(64)]
    public string Country { get; set; }

    [MaxLength(64)]
    public string City { get; set; }

    [MaxLength(64)]
    public string Street { get; set; }

    public int? ProfilePhotoId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
