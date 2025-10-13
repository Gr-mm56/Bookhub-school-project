using BusinessLayer.Models.Book.Responses;

namespace BusinessLayer.Models.Author.Responses;
public class AuthorBooksDto : AuthorDto
{
    public List<BookDto> Books { get; set; }
}
