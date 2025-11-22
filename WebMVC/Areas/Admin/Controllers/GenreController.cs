using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Areas.Admin.Controllers;

public class GenreController : AdminController
{
    private readonly IGenreService _genreService;
    private readonly IBookService _bookService;

    public GenreController(IGenreService genreService, IBookService bookService)
    {
        _genreService = genreService;
        _bookService = bookService;
    }

    // GET: Admin/Genre
    public async Task<IActionResult> Index()
    {
        var genres = await _genreService.GetAllAsync(0, 0);
        var model = new Models.GenresViewModel
        {
            Genres = genres.Items.OrderBy(x => x.Id).ToList()
        };
        return View(model);
    }

    // GET: Admin/Genre/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Genre/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GenreRequestDto genreDto)
    {
        if (!ModelState.IsValid)
        {
            return View(genreDto);
        }

        await _genreService.CreateAsync(genreDto);
        TempData["SuccessMessage"] = "Genre created successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Admin/Genre/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var genre = await _genreService.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        var genreDto = new GenreRequestDto
        {
            Name = genre.Name
        };

        ViewBag.GenreId = id;
        return View(genreDto);
    }

    // POST: Admin/Genre/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, GenreRequestDto genreDto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.GenreId = id;
            return View(genreDto);
        }

        var result = await _genreService.UpdateAsync(id, genreDto);
        if (result == null)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Genre updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Admin/Genre/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var genre = await _genreService.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    // POST: Admin/Genre/Delete/5
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