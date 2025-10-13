using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Rating.Requests;

public class RatingRequestDto
{
    [Range(1, 5)]
    public int Stars { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
}