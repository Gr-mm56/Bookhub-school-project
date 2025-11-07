using BusinessLayer.Models.Image.Responses;

namespace BusinessLayer.Models.Author.Responses;

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ImageDto? ProfilePhoto { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}