using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Genre.Responses;
using BusinessLayer.Models.Image.Responses;
using BusinessLayer.Models.Publisher.Responses;
using BusinessLayer.Services.Interfaces;

namespace BusinessLayer.Services.Implementations;

public class BookManagementFacade : IBookManagementFacade
{
    private readonly IBookService _bookService;
    private readonly IGenreService _genreService;
    private readonly IImageService _imageService;
    private readonly IPublisherService _publisherService;
    private readonly IAuthorService _authorService;

    public BookManagementFacade(
        IBookService bookService,
        IGenreService genreService,
        IImageService imageService,
        IPublisherService publisherService,
        IAuthorService authorService)
    {
        _bookService = bookService;
        _genreService = genreService;
        _imageService = imageService;
        _publisherService = publisherService;
        _authorService = authorService;
    }

    public async Task<(List<BookDto> Items, int Total)> GetAllBooksAsync(int pageSize, int offset)
    {
        var result = await _bookService.GetAllAsync(pageSize, offset);
        return (result.Items.ToList(), result.Total);
    }

    public async Task<BookDetailDto?> GetBookByIdAsync(int id)
    {
        return await _bookService.GetByIdAsync(id);
    }

    public async Task<BookDto> CreateBookAsync(BookRequestDto request)
    {
        return await _bookService.CreateAsync(request);
    }

    public async Task<BookDto?> UpdateBookAsync(int id, BookRequestDto request)
    {
        return await _bookService.UpdateAsync(id, request);
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        return await _bookService.DeleteAsync(id);
    }

    public async Task<List<GenreDto>> GetAllGenresAsync()
    {
        var result = await _genreService.GetAllAsync(0, 0);
        return result.Items.ToList();
    }

    public async Task<List<ImageDto>> GetAllImagesAsync()
    {
        var result = await _imageService.GetAllAsync(0, 0);
        return result.Items.ToList();
    }

    public async Task<List<PublisherBooksDto>> GetAllPublishersAsync()
    {
        var result = await _publisherService.GetAllAsync(0, 0);
        return result.Items.ToList();
    }

    public async Task<List<AuthorBooksDto>> GetAllAuthorsAsync()
    {
        var result = await _authorService.GetAllAsync(0, 0);
        return result.Items.ToList();
    }
    
    public async Task<(List<GenreDto> Genres, List<ImageDto> Images, List<PublisherBooksDto> Publishers, List<AuthorBooksDto> Authors)> GetAllDropdownOptionsAsync()
    {
        var genresTask = _genreService.GetAllAsync(0, 0);
        var imagesTask = _imageService.GetAllAsync(0, 0);
        var publishersTask = _publisherService.GetAllAsync(0, 0);
        var authorsTask = _authorService.GetAllAsync(0, 0);

        await Task.WhenAll(genresTask, imagesTask, publishersTask, authorsTask);

        return (
            genresTask.Result.Items.ToList(),
            imagesTask.Result.Items.ToList(),
            publishersTask.Result.Items.ToList(),
            authorsTask.Result.Items.ToList()
        );
    }
}

