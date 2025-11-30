using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Areas.Admin.Mappers;
using WebMVC.Areas.Admin.Models.Image;

namespace WebMVC.Areas.Admin.Controllers;

public class ImageController : AdminController
{
    private readonly IImageService _imageService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private const int PageSize = 12;
    private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

    public ImageController(IImageService imageService, IWebHostEnvironment webHostEnvironment)
    {
        _imageService = imageService;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        if (page < 1)
            page = 1;

        var offset = (page - 1) * PageSize;
        var pagedResult = await _imageService.GetAllAsync(PageSize, offset);

        var viewModel = ImageViewModelMapper.ToListViewModel(pagedResult.Items.ToList());
        
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)pagedResult.Total / PageSize);
        ViewBag.TotalCount = pagedResult.Total;
        ViewBag.PageSize = PageSize;

        return View(viewModel);
    }

    public IActionResult Create()
    {
        var viewModel = new ImageCreateEditViewModel();
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ImageCreateEditViewModel model)
    {
        if (model.File == null || model.File.Length == 0)
        {
            ModelState.AddModelError("File", "Please select a file to upload");
            return View(model);
        }

        if (model.File.Length > MaxFileSize)
        {
            ModelState.AddModelError("File", "File size must not exceed 5 MB");
            return View(model);
        }

        var fileExtension = Path.GetExtension(model.File.FileName).ToLower();
        if (!AllowedExtensions.Contains(fileExtension))
        {
            ModelState.AddModelError("File", "Only image files (jpg, jpeg, png, gif, webp) are allowed");
            return View(model);
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            var relativeFilePath = $"assets/uploads/{fileName}";
            var imageRequestDto = new BusinessLayer.Models.Image.Requests.ImageRequestDto
            {
                FileUrl = relativeFilePath
            };
            await _imageService.CreateAsync(imageRequestDto);

            TempData["SuccessMessage"] = "Image uploaded successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("File", $"Error uploading file: {ex.Message}");
            return View(model);
        }
    }

    public async Task<IActionResult> Detail(int id)
    {
        var imageDto = await _imageService.GetByIdAsync(id);
        if (imageDto == null)
        {
            return NotFound();
        }

        var viewModel = ImageViewModelMapper.ToListItemViewModel(imageDto);
        return View(viewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var imageDto = await _imageService.GetByIdAsync(id);
        if (imageDto == null)
        {
            return NotFound();
        }

        var viewModel = ImageViewModelMapper.ToDeleteViewModel(imageDto);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(ImageDeleteViewModel model)
    {
        try
        {
            var imageDto = await _imageService.GetByIdAsync(model.Id);
            if (imageDto != null)
            {
                if (imageDto.FileUrl.Contains("assets/uploads/"))
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, imageDto.FileUrl);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
            }

            var deleted = await _imageService.DeleteAsync(model.Id);
            if (!deleted)
            {
                TempData["ErrorMessage"] = "Cannot delete this image as it is being used by other entities.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Image deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error deleting image: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
}

