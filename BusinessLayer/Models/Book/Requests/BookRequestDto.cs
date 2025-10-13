using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Book.Requests;

public class BookRequestDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required]
    [MaxLength(17)]
    public string ISBN { get; set; }

    [MaxLength(300)]
    public string? Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public double Price { get; set; }

    public int ImageId { get; set; }

    public int PublisherId { get; set; }

    public List<int> GenreIds { get; set; } = new List<int>();

    public List<int> AuthorIds { get; set; } = new List<int>();
}
