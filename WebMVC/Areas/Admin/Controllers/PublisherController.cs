using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Areas.Admin.Mappers;
using WebMVC.Areas.Admin.Models.Publisher;

namespace WebMVC.Areas.Admin.Controllers;

public class PublisherController : AdminController
{
    private readonly IPublisherService _publisherService;
    private readonly IImageService _imageService;
    private readonly IBookService _bookService;
    private const int PageSize = 10;

    public PublisherController(
        IPublisherService publisherService,
        IImageService imageService,
        IBookService bookService)
    {
        _publisherService = publisherService;
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
        var result = await _publisherService.GetAllAsync(PageSize, offset);

        var viewModel = PublisherViewModelMapper.ToListViewModel(result.Items.ToList());

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)result.Total / PageSize);
        ViewBag.TotalCount = result.Total;
        ViewBag.PageSize = PageSize;

        return View(viewModel);
    }

    public async Task<IActionResult> Create()
    {
        var publisherViewModel = new PublisherCreateEditViewModel
        {
            Name = "",
            Address = ""
        };

        var viewModel = await LoadPublisherOptionsAsync(publisherViewModel);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PublisherCreateEditViewModelWithOptions model)
    {
        if (!ModelState.IsValid)
        {
            var reloadedModel = await LoadPublisherOptionsAsync(model.Publisher);
            return View(reloadedModel);
        }

        var publisherRequestDto = PublisherViewModelMapper.ToRequestDto(model.Publisher);
        await _publisherService.CreateAsync(publisherRequestDto);

        TempData["SuccessMessage"] = "Publisher created successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var publisher = await _publisherService.GetByIdAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }

        var publisherViewModel = PublisherViewModelMapper.ToDtoToCreateEditViewModel(publisher);
        var viewModel = await LoadPublisherOptionsAsync(publisherViewModel);
        ViewBag.PublisherId = id;
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PublisherCreateEditViewModelWithOptions model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.PublisherId = id;
            var reloadedModel = await LoadPublisherOptionsAsync(model.Publisher);
            return View(reloadedModel);
        }

        var publisherRequestDto = PublisherViewModelMapper.ToRequestDto(model.Publisher);
        var result = await _publisherService.UpdateAsync(id, publisherRequestDto);

        if (result == null)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Publisher updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var publisher = await _publisherService.GetByIdAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }

        var viewModel = PublisherViewModelMapper.ToDeleteViewModel(publisher);
        ViewBag.BookCount = publisher.Books?.Count ?? 0;
        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var result = await _publisherService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Publisher deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Delete), new { id });
        }
    }

    private async Task<PublisherCreateEditViewModelWithOptions> LoadPublisherOptionsAsync(PublisherCreateEditViewModel publisherViewModel)
    {
        var imagesResult = await _imageService.GetAllAsync(0, 0);
        var booksResult = await _bookService.GetAllAsync(0, 0);

        return PublisherViewModelMapper.ToCreateEditViewModelWithOptions(
            publisherViewModel,
            imagesResult.Items.ToList(),
            booksResult.Items.ToList());
    }
}