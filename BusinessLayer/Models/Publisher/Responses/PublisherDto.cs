using BusinessLayer.Models.Image.Responses;

namespace BusinessLayer.Models.Publisher.Responses;

public class PublisherDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public ImageDto? ProfilePhoto { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}