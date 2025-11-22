using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Areas.Admin.Mappers;

namespace WebMVC.Areas.Admin.Controllers;

public class BookController : AdminController
{
    private readonly IBookService _bookService;
    private const int PageSize = 10;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    // GET: Admin/Book
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
}

