using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Areas.Admin.Mappers;
using WebMVC.Areas.Admin.Models.Book;

namespace WebMVC.Areas.Admin.Controllers;

public class BookController : AdminController
{
    private readonly IBookService _bookService;
    private readonly IGenreService _genreService;
    private readonly IImageService _imageService;
    private readonly IPublisherService _publisherService;
    private readonly IAuthorService _authorService;
    private const int PageSize = 10;

    public BookController(
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

    public async Task<IActionResult> Index(int page = 1)
    {
        if (page < 1)
            page = 1;

        var offset = (page - 1) * PageSize;
        var pagedResult = await _bookService.GetAllAsync(PageSize, offset);

        var viewModel = BookViewModelMapper.ToListViewModel(pagedResult.Items.ToList());
        
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)pagedResult.Total / PageSize);
        ViewBag.TotalCount = pagedResult.Total;
        ViewBag.PageSize = PageSize;

        return View(viewModel);
    }

    public async Task<IActionResult> Create()
    {
        var bookViewModel = new BookCreateEditViewModel
        {
            Title = "",
            ISBN = "",
            Price = 0,
            PrimaryGenreId = 0
        };

        var viewModel = await LoadBookOptionsAsync(bookViewModel);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookCreateEditViewModelWithOptions model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var bookRequestDto = BookViewModelMapper.ToRequestDto(model.Book);
        await _bookService.CreateAsync(bookRequestDto);

        TempData["SuccessMessage"] = "Book created successfully!";
        return RedirectToAction(nameof(Index));
    }

    private async Task<BookCreateEditViewModelWithOptions> LoadBookOptionsAsync(BookCreateEditViewModel bookViewModel)
    {
        var genres = await _genreService.GetAllAsync(0, 0);
        var images = await _imageService.GetAllAsync(0, 0);
        var publishers = await _publisherService.GetAllAsync(0, 0);
        var authors = await _authorService.GetAllAsync(0, 0);

        return BookViewModelMapper.ToCreateEditViewModelWithOptions(
            bookViewModel,
            genres.Items.ToList(),
            images.Items.ToList(),
            publishers.Items.ToList(),
            authors.Items.ToList());
    }
}


