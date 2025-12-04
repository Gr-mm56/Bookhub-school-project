using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Genre.Responses;
using BusinessLayer.Models.Publisher.Responses;
using BusinessLayer.Models.Rating.Responses;

namespace BusinessLayer.Models.Book.Responses;

public class BookDetailDto : BookDto
{
    public string ISBN { get; set; }
    public List<GenreDto> Genres { get; set; }
    public List<AuthorDto> Authors { get; set; }
    public PublisherDto? Publisher { get; set; }
    public List<RatingDto> Ratings { get; set; }
}