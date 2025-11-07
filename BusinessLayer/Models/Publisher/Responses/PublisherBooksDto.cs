using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Image.Responses;

namespace BusinessLayer.Models.Publisher.Responses;

public class PublisherBooksDto : PublisherDto
{
    public List<BookDto> Books { get; set; }
}