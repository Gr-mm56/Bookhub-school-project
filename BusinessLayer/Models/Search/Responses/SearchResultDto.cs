using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Publisher.Responses;

namespace BusinessLayer.Models.Search.Responses;

public class SearchResultDto
{
    public List<BookDetailDto> Books { get; set; } = [];
    public PaginationInfo BookPagination { get; set; } = new(12);

    public List<AuthorBooksDto> Authors { get; set; } = [];
    public PaginationInfo AuthorPagination { get; set; } = new(5);

    public List<PublisherBooksDto> Publishers { get; set; } = [];
    public PaginationInfo PublisherPagination { get; set; } = new(5);
}