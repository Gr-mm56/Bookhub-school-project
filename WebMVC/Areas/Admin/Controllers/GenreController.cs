using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Areas.Admin.Mappers;
using WebMVC.Areas.Admin.Models.Genre;

namespace WebMVC.Areas.Admin.Controllers;

public class GenreController : AdminController
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task<IActionResult> Index()
    {
        var genres = await _genreService.GetAllAsync(0, 0);
        var viewModel = GenreViewModelMapper.ToListViewModel(genres.Items.ToList());
        return View(viewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GenreCreateEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var genreDto = new GenreRequestDto
        {
            Name = viewModel.Name
        };

        await _genreService.CreateAsync(genreDto);
        TempData["SuccessMessage"] = "Genre created successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var genre = await _genreService.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        var viewModel = GenreViewModelMapper.ToCreateEditViewModel(genre);
        ViewBag.GenreId = id;
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, GenreCreateEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.GenreId = id;
            return View(viewModel);
        }

        var genreDto = new GenreRequestDto
        {
            Name = viewModel.Name
        };

        var result = await _genreService.UpdateAsync(id, genreDto);
        if (result == null)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Genre updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var genre = await _genreService.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        var viewModel = GenreViewModelMapper.ToDeleteViewModel(genre);
        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _genreService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Genre deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
