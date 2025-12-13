using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Genre.Responses;
using BusinessLayer.Models.Image.Responses;
using BusinessLayer.Models.Publisher.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IBookManagementFacade
{
    Task<(List<BookDto> Items, int Total)> GetAllBooksAsync(int pageSize, int offset);

    Task<BookDetailDto?> GetBookByIdAsync(int id);

    Task<BookDto> CreateBookAsync(BookRequestDto request);

    Task<BookDto?> UpdateBookAsync(int id, BookRequestDto request);

    Task<bool> DeleteBookAsync(int id);

    Task<List<GenreDto>> GetAllGenresAsync();

    Task<List<ImageDto>> GetAllImagesAsync();

    Task<List<PublisherBooksDto>> GetAllPublishersAsync();

    Task<List<AuthorBooksDto>> GetAllAuthorsAsync();

    /// <summary>
    /// Get all dropdown options (genres, images, publishers, and authors) in parallel for better performance.
    /// </summary>
    Task<(List<GenreDto> Genres, List<ImageDto> Images, List<PublisherBooksDto> Publishers, List<AuthorBooksDto> Authors)> GetAllDropdownOptionsAsync();
}