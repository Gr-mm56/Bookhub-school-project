using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Image.Responses;

namespace BusinessLayer.Models.Publisher.Responses;

public class PublisherDetailDto: PublisherDto
{
    public string address { get; set; } = string.Empty;
    public ImageDto? ProfilePhoto { get; set; }
    public List<BookDto>? Books { get; set; }
}