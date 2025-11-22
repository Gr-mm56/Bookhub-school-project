using BusinessLayer.Models.Image.Responses;

namespace BusinessLayer.Models.Book.Responses;

public class BookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public double Price { get; set; }
    public ImageDto? Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int PrimaryGenreId { get; set; }
}