using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Areas.Admin.Mappers;
using WebMVC.Areas.Admin.Models.Order;

namespace WebMVC.Areas.Admin.Controllers;

public class OrderController : AdminController
{
    private readonly ICartService _cartService;
    private readonly IUserService _userService;
    private readonly IPurchaseItemService _purchaseItemService;
    private const int PageSize = 10;

    public OrderController(
        ICartService cartService,
        IUserService userService,
        IPurchaseItemService purchaseItemService
    ) {
        _cartService = cartService;
        _userService = userService;
        _purchaseItemService = purchaseItemService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        if (page < 1)
            page = 1;

        var offset = (page - 1) * PageSize;
        var pagedResult = await _cartService.GetAllAsync(PageSize, offset);

        var viewModel = OrderViewModelMapper.ToListViewModel(pagedResult.Items.ToList());

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)pagedResult.Total / PageSize);
        ViewBag.TotalCount = pagedResult.Total;
        ViewBag.PageSize = PageSize;

        return View(viewModel);
    }

    public async Task<IActionResult> Create()
    {
        var orderViewModel = new OrderCreateEditViewModel
        {
            Title = "",
            ISBN = "",
            Price = 0,
            PrimaryGenreId = 0
        };

        var viewModel = await LoadOrderOptionsAsync(orderViewModel);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderCreateEditViewModelWithOptions model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var orderRequestDto = OrderViewModelMapper.ToRequestDto(model.Order);
        await _cartService.CreateAsync(orderRequestDto);

        TempData["SuccessMessage"] = "Order created successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Admin/Order/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var order = await _cartService.GetByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        var orderViewModel = new OrderCreateEditViewModel
        {
            Title = order.Title,
            ISBN = order.ISBN,
            Description = order.Description,
            Price = order.Price,
            PrimaryGenreId = order.PrimaryGenreId,
            ImageId = order.Image?.Id ?? null,
            PublisherId = order.Publisher?.Id ?? null,
            GenreIds = order.Genres.Select(g => g.Id).ToList(),
            AuthorIds = order.Authors.Select(a => a.Id).ToList()
        };

        var viewModel = await LoadOrderOptionsAsync(orderViewModel);
        ViewBag.OrderId = id;
        return View(viewModel);
    }

    // POST: Admin/Order/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, OrderCreateEditViewModelWithOptions model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.OrderId = id;
            return View(model);
        }

        var orderRequestDto = OrderViewModelMapper.ToRequestDto(model.Order);
        var result = await _cartService.UpdateAsync(id, orderRequestDto);

        if (result == null)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Order updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var order = await _cartService.GetByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        var viewModel = OrderViewModelMapper.ToDeleteViewModel(order);
        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _cartService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Order deleted successfully!";
        return RedirectToAction(nameof(Index));
    }

    private async Task<OrderCreateEditViewModelWithOptions> LoadOrderOptionsAsync(OrderCreateEditViewModel orderViewModel)
    {
        var genres = await _genreService.GetAllAsync(0, 0);
        var images = await _imageService.GetAllAsync(0, 0);
        var publishers = await _publisherService.GetAllAsync(0, 0);
        var authors = await _authorService.GetAllAsync(0, 0);

        return OrderViewModelMapper.ToCreateEditViewModelWithOptions(
            orderViewModel,
            genres.Items.ToList(),
            images.Items.ToList(),
            publishers.Items.ToList(),
            authors.Items.ToList());
    }
}
