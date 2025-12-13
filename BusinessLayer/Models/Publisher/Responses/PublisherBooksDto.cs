using BusinessLayer.Models.Book.Responses;

namespace BusinessLayer.Models.Publisher.Responses;

public class PublisherBooksDto : PublisherDto
{
    public List<BookDto> Books { get; set; }
}