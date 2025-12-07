using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Areas.Admin.Mappers;
using WebMVC.Areas.Admin.Models.Book;

namespace WebMVC.Areas.Admin.Controllers;

public class BookController : AdminController
{
    private readonly IBookManagementFacade _bookManagementFacade;
    private const int PageSize = 10;

    public BookController(IBookManagementFacade bookManagementFacade)
    {
        _bookManagementFacade = bookManagementFacade;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        if (page < 1)
        {
            page = 1;
        }
        var offset = (page - 1) * PageSize;
        var (items, total) = await _bookManagementFacade.GetAllBooksAsync(PageSize, offset);

        var viewModel = BookViewModelMapper.ToListViewModel(items);
        
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)total / PageSize);
        ViewBag.TotalCount = total;
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
        await _bookManagementFacade.CreateBookAsync(bookRequestDto);

        TempData["SuccessMessage"] = "Book created successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Admin/Book/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookManagementFacade.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        var bookViewModel = BookViewModelMapper.ToDtoToCreateEditViewModel(book);
        var viewModel = await LoadBookOptionsAsync(bookViewModel);
        ViewBag.BookId = id;
        return View(viewModel);
    }

    // POST: Admin/Book/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BookCreateEditViewModelWithOptions model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.BookId = id;
            return View(model);
        }

        var bookRequestDto = BookViewModelMapper.ToRequestDto(model.Book);
        var result = await _bookManagementFacade.UpdateBookAsync(id, bookRequestDto);
        
        if (result == null)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Book updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var book = await _bookManagementFacade.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        var viewModel = BookViewModelMapper.ToDeleteViewModel(book);
        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _bookManagementFacade.DeleteBookAsync(id);
        if (!result)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Book deleted successfully!";
        return RedirectToAction(nameof(Index));
    }

    private async Task<BookCreateEditViewModelWithOptions> LoadBookOptionsAsync(BookCreateEditViewModel bookViewModel)
    {
        var (genres, images, publishers, authors) = await _bookManagementFacade.GetAllDropdownOptionsAsync();

        return BookViewModelMapper.ToCreateEditViewModelWithOptions(
            bookViewModel,
            genres,
            images,
            publishers,
            authors);
    }
}
