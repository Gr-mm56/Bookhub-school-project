using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Rating.Responses;

public class RatingDto
{
    public int Id { get; set; }

    [Range(0, 5)]
    public int Stars { get; set; }

    public int BookId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
