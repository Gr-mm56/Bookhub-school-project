using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Publisher.Responses;
using BusinessLayer.Models.Search.Responses;
using BusinessLayer.Services.Interfaces;

namespace BusinessLayer.Services.Implementations;

public class SearchFacade : ISearchFacade
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    private readonly IPublisherService _publisherService;

    public SearchFacade(
        IBookService bookService,
        IAuthorService authorService,
        IPublisherService publisherService)
    {
        _bookService = bookService;
        _authorService = authorService;
        _publisherService = publisherService;
    }


    public async Task<SearchResultDto> SearchAsync(
        string? searchTerm = null,
        int bookPageNumber = 1,
        int authorPageNumber = 1,
        int publisherPageNumber = 1)
    {
        bookPageNumber = Math.Max(1, bookPageNumber);
        authorPageNumber = Math.Max(1, authorPageNumber);
        publisherPageNumber = Math.Max(1, publisherPageNumber);

        const int bookPageSize = 12;
        const int authorPageSize = 5;
        const int publisherPageSize = 5;

        var bookTask = SearchBooksAsync(searchTerm, bookPageNumber, bookPageSize);
        var authorTask = SearchAuthorsAsync(searchTerm, authorPageNumber, authorPageSize);
        var publisherTask = SearchPublishersAsync(searchTerm, publisherPageNumber, publisherPageSize);

        await Task.WhenAll(bookTask, authorTask, publisherTask);

        return new SearchResultDto
        {
            Books = bookTask.Result.books,
            BookPagination = bookTask.Result.pagination,
            Authors = authorTask.Result.authors,
            AuthorPagination = authorTask.Result.pagination,
            Publishers = publisherTask.Result.publishers,
            PublisherPagination = publisherTask.Result.pagination
        };
    }

    private async Task<(List<BookDetailDto> books, PaginationInfo pagination)> SearchBooksAsync(
        string? searchTerm,
        int pageNumber,
        int pageSize)
    {
        var offset = (pageNumber - 1) * pageSize;
        var searchDto = new BookSearchDto
        {
            Limit = pageSize,
            Offset = offset,
            SearchTerm = searchTerm
        };

        var result = await _bookService.SearchBooksAsync(searchDto);

        return (result.Items.ToList(), new PaginationInfo(pageSize)
        {
            PageNumber = pageNumber,
            TotalCount = result.Total
        });
    }

    private async Task<(List<AuthorBooksDto> authors, PaginationInfo pagination)> SearchAuthorsAsync(
        string? searchTerm,
        int pageNumber,
        int pageSize)
    {
        var offset = (pageNumber - 1) * pageSize;
        var result = await _authorService.GetAllAsync(pageSize, offset);

        List<AuthorBooksDto> authors;

        if (!string.IsNullOrEmpty(searchTerm))
        {
            authors = result.Items
                .Where(a => a.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                           a.Surname.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        else
        {
            authors = result.Items.ToList();
        }

        return (authors, new PaginationInfo(pageSize)
        {
            PageNumber = pageNumber,
            TotalCount = result.Total
        });
    }

    private async Task<(List<PublisherBooksDto> publishers, PaginationInfo pagination)> SearchPublishersAsync(
        string? searchTerm,
        int pageNumber,
        int pageSize)
    {
        var offset = (pageNumber - 1) * pageSize;
        var result = await _publisherService.GetAllAsync(pageSize, offset);

        List<PublisherBooksDto> publishers;

        if (!string.IsNullOrEmpty(searchTerm))
        {
            publishers = result.Items
                .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        else
        {
            publishers = result.Items.ToList();
        }

        return (publishers, new PaginationInfo(pageSize)
        {
            PageNumber = pageNumber,
            TotalCount = result.Total
        });
    }
}

