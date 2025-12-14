using BusinessLayer.Models.WishlistItem.Requests;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Mappers;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class WishlistController : Controller
{
    private readonly ILogger<OrderController> _logger;
    private readonly IWishlistItemService _wishlistItemService;
    private SignInManager<LocalIdentityUser> _signInManager;
    private UserManager<LocalIdentityUser> _userManager;

    public WishlistController(
        ILogger<OrderController> logger,
        IWishlistItemService wishlistItemService,
        SignInManager<LocalIdentityUser> signInManager,
        UserManager<LocalIdentityUser> userManager
    )
    {
        _logger = logger;
        _wishlistItemService = wishlistItemService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var userId = await ValidateLoginAndGetUserId();
            var wishlistItems = await _wishlistItemService.GetWishlistByUserIdAsync(userId);

            var viewModel = new WishlistViewModel
            {
                WishlistItems = WishlistMapper.ToWishlistViewModels(wishlistItems)
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading wishlist");
            return View(new WishlistViewModel());
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist(int bookId)
    {
        try
        {
            var userId = await ValidateLoginAndGetUserId();
            var wishlistItems = await _wishlistItemService.GetWishlistByUserIdAsync(userId);
            var existingItem = wishlistItems.FirstOrDefault(pi => pi.BookId == bookId);

            if (existingItem != null)
            {
                TempData["Success"] = "Book is already in your wishlist!";
                return RedirectToAction("Index");
            }

            var wishlistItemDto = new WishlistItemCreateDto
            {
                UserId = userId,
                BookId = bookId,
            };

            await _wishlistItemService.CreateAsync(wishlistItemDto);

            TempData["Success"] = "Item added to wishlist successfully!";
            return RedirectToAction("Index");
        } catch (Exception ex) {
            _logger.LogError(ex, "Error loading wishlist");
            return View("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromWishlist(int bookId)
    {
        try {
            var userId = await ValidateLoginAndGetUserId();

            await _wishlistItemService.DeleteByUserBookIdAsync(userId, bookId);

            TempData["Success"] = "Item removed from wishlist.";
            return RedirectToAction("Index");

        } catch (Exception ex) {
            _logger.LogError(ex, "Error loading wishlist");
            return View("Index");
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
