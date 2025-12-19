using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Areas.Admin.Mappers;
using WebMVC.Areas.Admin.Models.Author;

namespace WebMVC.Areas.Admin.Controllers;

public class AuthorController : AdminController
{
    private readonly IAuthorService _authorService;
    private readonly IImageService _imageService;
    private readonly IBookService _bookService;
    private const int PageSize = 10;

    public AuthorController(
        IAuthorService authorService,
        IImageService imageService,
        IBookService bookService)
    {
        _authorService = authorService;
        _imageService = imageService;
        _bookService = bookService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        if (page < 1)
        {
            page = 1;
        }
        var offset = (page - 1) * PageSize;
        var result = await _authorService.GetAllAsync(PageSize, offset);

        var viewModel = AuthorViewModelMapper.ToListViewModel(result.Items.ToList());

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)result.Total / PageSize);
        ViewBag.TotalCount = result.Total;
        ViewBag.PageSize = PageSize;

        return View(viewModel);
    }

    public async Task<IActionResult> Create()
    {
        var authorViewModel = new AuthorCreateEditViewModel
        {
            Name = "",
            Surname = ""
        };

        var viewModel = await LoadAuthorOptionsAsync(authorViewModel);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AuthorCreateEditViewModelWithOptions model)
    {
        if (!ModelState.IsValid)
        {
            var reloadedModel = await LoadAuthorOptionsAsync(model.Author);
            return View(reloadedModel);
        }

        var authorRequestDto = AuthorViewModelMapper.ToRequestDto(model.Author);
        await _authorService.CreateAsync(authorRequestDto);

        TempData["SuccessMessage"] = "Author created successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var author = await _authorService.GetByIdAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        var authorViewModel = AuthorViewModelMapper.ToDtoToCreateEditViewModel(author);
        var viewModel = await LoadAuthorOptionsAsync(authorViewModel);
        ViewBag.AuthorId = id;
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AuthorCreateEditViewModelWithOptions model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.AuthorId = id;
            var reloadedModel = await LoadAuthorOptionsAsync(model.Author);
            return View(reloadedModel);
        }

        var authorRequestDto = AuthorViewModelMapper.ToRequestDto(model.Author);
        var result = await _authorService.UpdateAsync(id, authorRequestDto);

        if (result == null)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Author updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var author = await _authorService.GetByIdAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        var viewModel = AuthorViewModelMapper.ToDeleteViewModel(author);
        ViewBag.BookCount = author.Books?.Count ?? 0;
        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var result = await _authorService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Author deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Delete), new { id });
        }
    }

    private async Task<AuthorCreateEditViewModelWithOptions> LoadAuthorOptionsAsync(AuthorCreateEditViewModel authorViewModel)
    {
        var imagesResult = await _imageService.GetAllAsync(0, 0);
        var booksResult = await _bookService.GetAllAsync(0, 0);

        return AuthorViewModelMapper.ToCreateEditViewModelWithOptions(
            authorViewModel,
            imagesResult.Items.ToList(),
            booksResult.Items.ToList());
    }
}