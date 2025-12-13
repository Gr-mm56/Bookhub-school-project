using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BusinessLayer.Models.PurchaseItem.Requests;
using BusinessLayer.Models.PurchaseItem.Responses;
using Microsoft.EntityFrameworkCore;
using WebMVC.Areas.Admin.Mappers;
using WebMVC.Mappers;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;
    private readonly ICartService _cartService;
    private readonly IUserService _userService;
    private readonly IPurchaseItemService _purchaseItemService;
    private readonly IWishlistItemService _wishlistItemService;
    private SignInManager<LocalIdentityUser> _signInManager;
    private UserManager<LocalIdentityUser> _userManager;

    public OrderController(
        ILogger<OrderController> logger,
        ICartService cartService,
        IUserService userService,
        IPurchaseItemService purchaseItemService,
        IWishlistItemService wishlistItemService,
        SignInManager<LocalIdentityUser> signInManager,
        UserManager<LocalIdentityUser> userManager
    ) {
        _logger = logger;
        _cartService = cartService;
        _userService = userService;
        _purchaseItemService = purchaseItemService;
        _wishlistItemService = wishlistItemService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<IActionResult> Cart()
    {
        try
        {
            var userId = await ValidateLoginAndGetUserId();
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                return View(new CartViewModel());
            }

            var viewModel = CartMapper.ToCartViewModel(cart);

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading cart");
            return View(new List<CartViewModel>());
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int bookId, int quantity = 1)
    {
        try
        {
            var userId = await ValidateLoginAndGetUserId();
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                _logger.LogWarning("Cart not found for user {UserId}", userId);
                return RedirectToAction("Cart");
            }

            var purchaseItemDto = new PurchaseItemCreateDto
            {
                CartId = cart.Id,
                Count = quantity,
                BookId = bookId,
            };

            await _purchaseItemService.CreateAsync(purchaseItemDto);

            TempData["Success"] = "Item added to cart successfully!";
            return RedirectToAction("Cart");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding to cart");
            TempData["Error"] = "Failed to add item to cart.";
            return RedirectToAction("Cart");
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(int bookId)
    {
        try
        {
            var userId = await ValidateLoginAndGetUserId();
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                _logger.LogWarning("Cart not found for user {UserId}", userId);
                return RedirectToAction("Cart");
            }

            await _purchaseItemService.DeleteByItemIdAsync(bookId, cart.Id);

            TempData["Success"] = "Item removed from cart.";
            return RedirectToAction("Cart");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing from cart");
            TempData["Error"] = "Failed to remove item.";
            return RedirectToAction("Cart");
        }
    }

    private async Task<int> ValidateLoginAndGetUserId()
    {
        try
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return -1;
            }

            var identityUser = await _userManager.Users
                .Include(u => u.User)
                .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

            if (identityUser == null)
            {
                return -1;
            }

            return identityUser.UserId;
        }
        catch (Exception e)
        {
            return -1;
        }
    }
}
