using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<LocalIdentityUser> _userManager;
    private readonly SignInManager<LocalIdentityUser> _signInManager;
    private readonly ICartService _cartService;

    public AccountController(
        UserManager<LocalIdentityUser> userManager,
        SignInManager<LocalIdentityUser> signInManager,
        ICartService cartService
    ) {
        _userManager = userManager;
        _signInManager = signInManager;
        _cartService = cartService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new LocalIdentityUser
        {
            UserName = model.Username,
            Email = model.Email,
            User = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Country = model.Country,
                City = model.City,
                Street = model.Street,
                CreatedAt = DateTime.Now
            }
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, RoleInitializer.UserRole);

            if (model.IsAdmin)
            {
                await _userManager.AddToRoleAsync(user, RoleInitializer.AdminRole);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            var userId = user.User.Id;

            // create default empty cart for user
            CartCreateDto cartDto = new CartCreateDto
            {
                UserId = userId,
                TotalValue = 0,
                PaymentStatus = 0,
            };

            await _cartService.CreateAsync(cartDto);

            return RedirectToAction(nameof(Login), nameof(AccountController).Replace("Controller", ""));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result =
            await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe,
                lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
    }

    public IActionResult LoginSuccess()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        // I hope for the purpose of this project, actual email does not need to be sent
        TempData["ResetToken"] = token;
        TempData["ResetEmail"] = model.Email;

        return RedirectToAction(nameof(ForgotPasswordConfirmation));
    }

    [HttpGet]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string? token, string? email)
    {
        if (token == null || email == null)
        {
            return RedirectToAction(nameof(Login));
        }

        var model = new ResetPasswordViewModel
        {
            Token = token,
            Email = email,
            Password = string.Empty,
            ConfirmPassword = string.Empty
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }
}
