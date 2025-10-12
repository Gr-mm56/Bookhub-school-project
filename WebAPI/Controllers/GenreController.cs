using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class GenreController : BaseController<Genre>
{
    private readonly IGenreRepository _genreRepository;

    public GenreController(IGenreRepository genreRepository) : base(genreRepository)
    {
        _genreRepository = genreRepository;
    }

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetGenres() => await GetAll();

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> SearchGenres([FromQuery] string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("Name query parameter is required.");
        }

        var genres = await _genreRepository.SearchGenresAsync(name);
        var result = new { Genres = genres.Select(g => new { g.Id, g.Name }) };
        return Ok(result);
    }

    [HttpGet]
    [Route("details/{id:int}")]
    public async Task<IActionResult> GetGenreWithBooks(int id)
    {
        var genre = await _genreRepository.GetGenreWithBooksAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        return Ok(new
        {
            GenreId = genre.Id,
            GenreName = genre.Name,
            Books = genre.Books.Select(b => new
            {
                BookId = b.Id,
                BookTitle = b.Title,
                BookDescription = b.Description,
                BookPrice = b.Price
            })
        });
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetGenreById(int id) => await Get(id);

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateGenre([FromBody] Genre newGenre) => await Insert(newGenre);

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] Genre updatedGenre) =>
        await Update(id, updatedGenre);

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id) => await Delete(id);
}