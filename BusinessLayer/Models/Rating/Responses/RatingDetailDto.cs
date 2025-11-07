using System.ComponentModel.DataAnnotations;
using BusinessLayer.Models.Book.Responses;

namespace BusinessLayer.Models.Rating.Responses;

public class RatingDetailDto
{
    public int Id { get; set; }
    public int Stars { get; set; }
    [Required]
    public BookDto Book { get; set; }
    // public UserDto User { get; set; } todo: uncomment when user model is ready
}