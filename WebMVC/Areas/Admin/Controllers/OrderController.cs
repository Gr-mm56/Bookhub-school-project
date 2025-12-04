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
    private readonly IBookService _bookService;
    private const int PageSize = 10;

    public OrderController(
        ICartService cartService,
        IUserService userService,
        IPurchaseItemService purchaseItemService,
        IBookService bookService
    ) {
        _cartService = cartService;
        _userService = userService;
        _purchaseItemService = purchaseItemService;
        _bookService = bookService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        page = page < 1 ? 1 : page;

        var offset = (page - 1) * PageSize;

        // Get all CartDtos with OrderId filled
        var pagedResult = await _cartService.GetAllOrdersAsync(PageSize, offset);

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
            TotalValue = 0,
            OrderDate = DateTime.Now,
            PaymentStatus = false,
            BookIds = new List<int>(),
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

        var orderRequestDto = OrderViewModelMapper.ToCreateDto(model.Order);
        await _cartService.CreateOrderAsync(orderRequestDto);

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
            UserId = order.UserId,
            TotalValue = order.TotalValue,
            OrderDate = order.OrderDate,
            PaymentStatus = order.PaymentStatus,
            BookIds = order.PurchaseItems?.Select(p => p.BookId).ToList() ?? new List<int>()
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

        var orderRequestDto = OrderViewModelMapper.ToUpdateDto(model.Order);
        var result = await _cartService.UpdateOrderAsync(id, orderRequestDto);

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
        var users = await _userService.GetAllAsync(0, 0);
        var purchaseItems = await _purchaseItemService.GetAllDetailsAsync(0);
        var books = await _bookService.GetAllAsync(0, 0);

        return OrderViewModelMapper.ToCreateEditViewModelWithOptions(
            orderViewModel,
            users.Items.ToList(),
            purchaseItems.Items
                .GroupBy(pi => pi.BookId)
                .Select(g => g.First())
                .ToList(),
            books.Items.ToList()
        );
    }
}
