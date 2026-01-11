using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Areas.Admin.Mappers;

namespace WebMVC.Areas.Admin.Controllers;

public class GiftCardController : AdminController
{
    private readonly IGiftCardService _giftCardService;
    private const int PageSize = 10;

    public GiftCardController(IGiftCardService giftCardService)
    {
        _giftCardService = giftCardService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        if (page < 1)
        {
            page = 1;
        }

        var offset = (page - 1) * PageSize;
        var result = await _giftCardService.GetAllAsync(PageSize, offset);

        var viewModel = GiftCardViewModelMapper.ToListViewModel(result.Items.ToList());

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)result.Total / PageSize);
        ViewBag.TotalCount = result.Total;
        ViewBag.PageSize = PageSize;

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var giftCard = await _giftCardService.GetByIdAsync(id);
        if (giftCard == null)
        {
            return NotFound();
        }

        var viewModel = GiftCardViewModelMapper.ToDeleteViewModel(giftCard);
        return View(viewModel);
    }

    public IActionResult Create()
    {
        var viewModel = new Models.GiftCard.GiftCardCreateEditViewModel
        {
            PriceReduction = 0,
            ValidFrom = DateTime.Now,
            ValidTo = DateTime.Now.AddMonths(3),
            NumberOfCoupons = 10
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Models.GiftCard.GiftCardCreateEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var createDto = GiftCardViewModelMapper.ToCreateDto(model);
            await _giftCardService.CreateAsync(createDto);

            TempData["SuccessMessage"] = "Gift card created successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var giftCard = await _giftCardService.GetByIdAsync(id);
        if (giftCard == null)
        {
            return NotFound();
        }

        var viewModel = GiftCardViewModelMapper.DtoToCreateEditViewModel(giftCard);
        ViewBag.GiftCardId = id;

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Models.GiftCard.GiftCardCreateEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.GiftCardId = id;
            return View(model);
        }

        try
        {
            var updateDto = GiftCardViewModelMapper.ToUpdateDto(model);
            var result = await _giftCardService.UpdateAsync(id, updateDto);

            if (result == null)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Gift card updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ViewBag.GiftCardId = id;
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var giftCard = await _giftCardService.GetByIdAsync(id);
        if (giftCard == null)
        {
            return NotFound();
        }

        var viewModel = GiftCardViewModelMapper.ToDeleteViewModel(giftCard);
        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var result = await _giftCardService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Gift card deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}