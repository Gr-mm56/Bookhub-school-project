using System.ComponentModel.DataAnnotations;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.User.Responses;

namespace BusinessLayer.Models.Rating.Responses;

public class RatingDetailDto
{
    public int Id { get; set; }

    public int Stars { get; set; }

    [Required]
    public required BookDto Book { get; set; }
    [Required]

     public required UserDto User { get; set; }

     public DateTime CreatedAt { get; set; }

     public DateTime UpdatedAt { get; set; }
}
