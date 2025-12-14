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
        var userId = await ValidateLoginAndGetUserId();
        var wishlist = await _wishlistItemService.GetWishlistByUserIdAsync(userId);

        if (!wishlist.Any())
        {
            return View(new WishlistViewModel());
        }

        var viewModel = WishlistMapper.ToWishlistViewModels(wishlist);

        return View(viewModel);
    }

    // [HttpPost]
    // public async Task<IActionResult> AddToWishlist()
    // {
    //     var userId = await ValidateLoginAndGetUserId();
    //     var wishlist = await _wishlistItemService.GetWishlistByUserIdAsync(userId);
    //
    //     if (!wishlist.Any())
    //     {
    //         return View(new WishlistViewModel());
    //     }
    // }
    //
    // [HttpPost]
    // public async Task<IActionResult> RemoveFromWishlist()
    // {
    //     var userId = await ValidateLoginAndGetUserId();
    //     var wishlist = await _wishlistItemService.GetWishlistByUserIdAsync(userId);
    //
    //     if (!wishlist.Any())
    //     {
    //         return View(new WishlistViewModel());
    //     }
    // }

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
