namespace BusinessLayer.Models.Image.Responses;

public class ImageDto
{
    public int Id { get; set; }
    public required string FileUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
