using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BusinessLayer.Models.Cart.Requests;
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
    private readonly IPurchaseItemService _purchaseItemService;
    private SignInManager<LocalIdentityUser> _signInManager;
    private UserManager<LocalIdentityUser> _userManager;

    public OrderController(
        ILogger<OrderController> logger,
        ICartService cartService,
        IPurchaseItemService purchaseItemService,
        SignInManager<LocalIdentityUser> signInManager,
        UserManager<LocalIdentityUser> userManager
    ) {
        _logger = logger;
        _cartService = cartService;
        _purchaseItemService = purchaseItemService;
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

            // On view of cart update TotalValue in DB too
            var updateDto = new CartUpdateDto
            {
                TotalValue = viewModel.TotalValue,
                PaymentStatus = viewModel.PaymentStatus,
            };

            await _cartService.UpdateAsync(cart.Id, updateDto);

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

            // Check if the book is already in the cart
            var cartBooks = await _purchaseItemService.GetAllDetailsByCartIdAsync(cart.Id);
            var existingItem = cartBooks.FirstOrDefault(pi => pi.BookId == bookId);

            if (existingItem != null)
            {
                var updateDto = new PurchaseItemUpdateDto()
                {
                    Count = existingItem.Count + quantity
                };

                await _purchaseItemService.UpdateAsync(existingItem.Id, updateDto);

                TempData["Success"] = "Quantity updated in cart!";
                return RedirectToAction("Cart");
            }

            // Book not in cart, create new purchase item
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
    public async Task<IActionResult> UpdateQuantity(int bookId, int quantity)
    {
        try
        {
            var userId = await ValidateLoginAndGetUserId();
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                TempData["Error"] = "Cart not found.";
                return RedirectToAction("Cart");
            }

            // Get cart items
            var cartItems = await _purchaseItemService.GetAllDetailsByCartIdAsync(cart.Id);
            var item = cartItems.FirstOrDefault(pi => pi.BookId == bookId);

            if (item != null) {
                var updateDto = new PurchaseItemUpdateDto
                {
                    Count = quantity
                };

                await _purchaseItemService.UpdateAsync(item.Id, updateDto);

                TempData["Success"] = "Quantity updated!";
            }
            else {
                TempData["Error"] = "Book not found in cart.";
            }

            return RedirectToAction("Cart");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating cart quantity");
            TempData["Error"] = "Failed to update quantity.";
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

    [HttpPost]
    public async Task<IActionResult> ApplyGiftCard(string code)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                TempData["GiftCardError"] = "Gift card code is required.";
                return RedirectToAction("Cart");
            }

            var userId = await ValidateLoginAndGetUserId();
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                TempData["GiftCardError"] = "Cart not found.";
                return RedirectToAction("Cart");
            }

            /* TODO just uncomment
            if (cart.GiftCardId != null)
            {
                TempData["GiftCardError"] = "Gift Card Code was already applied.";
                return RedirectToAction("Cart");
            }
            */

            /* TODO – get gift card from DB
            var giftCard = await _giftCardService.GetByCodeAsync(code);

            if (giftCard == null || !giftCard.IsActive)
            {
                TempData["GiftCardError"] = "Invalid or expired gift card.";
                return RedirectToAction("Cart");
            }

            var cartTotal = cart.TotalValue;
            var discount = giftCard.Value;

            // Validate if discount isn't too big comparing to the cart total value
            if (discount <= 0 || discount >= cartTotal)
            {
                TempData["GiftCardError"] =
                    "Gift card value must be smaller than cart total.";
                return RedirectToAction("Cart");
            }

            */

            // apply discount TODO
            // await _cartService.ApplyGiftCardAsync(cart.Id, discount);

            TempData["GiftCardSuccess"] = "Gift card applied successfully!";
            return RedirectToAction("Cart");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error applying gift card");
            TempData["GiftCardError"] = "Failed to apply gift card.";
            return RedirectToAction("Cart");
        }
    }

    public async Task<IActionResult> Checkout()
    {
        var userId = await ValidateLoginAndGetUserId();
        var cart = await _cartService.GetCartByUserIdAsync(userId);

        if (cart == null)
        {
            return RedirectToAction("Cart");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Submit(int paymentMethod)
    {
        var userId = await ValidateLoginAndGetUserId();
        var cart = await _cartService.GetCartByUserIdAsync(userId);

        if (cart == null)
        {
            return RedirectToAction("Cart");
        }

        if (paymentMethod <= 0)
        {
            TempData["GiftCardError"] = "Please select payment option.";
            return RedirectToAction("Checkout");
        }

        // Update cart payment method (Completed) + create order
        var orderCreateDto = new OrderCreateDto()
        {
            UserId = cart.UserId,
            TotalValue = cart.TotalValue,
            BookIds = cart.PurchaseItems!.Select(pi => pi.BookId).ToList(),
            PaymentStatus = 1,
        };

        var orderAsCartDto = await _cartService.CreateOrderAsync(orderCreateDto, cart.Id);

        // Create new cart for user
        CartCreateDto cartDto = new CartCreateDto
        {
            UserId = userId,
            TotalValue = 0,
            PaymentStatus = 0,
        };

        await _cartService.CreateAsync(cartDto);

        return RedirectToAction("OrderCreated", new { orderId = orderAsCartDto.OrderId });
    }

    public async Task<IActionResult> OrderCreated(int orderId)
    {
        return View(orderId);
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
