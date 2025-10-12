using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetGenres([FromQuery] int limit = 20, [FromQuery] int offset = 0)
    {
        var result = await _genreService.GetGenresAsync(limit, offset);
        return Ok(result);
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> SearchGenres([FromQuery] GenreSearchDto searchDto)
    {
        if (string.IsNullOrEmpty(searchDto.Name))
        {
            return BadRequest("Name query parameter is required.");
        }

        var result = await _genreService.SearchGenresAsync(searchDto);
        return Ok(result);
    }

    [HttpGet]
    [Route("details/{id:int}")]
    public async Task<IActionResult> GetGenreWithBooks(int id)
    {
        var genre = await _genreService.GetGenreWithBooksAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetGenreById(int id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateGenre([FromBody] GenreRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var genre = await _genreService.CreateGenreAsync(requestDto);
        return CreatedAtAction(nameof(GetGenreById), new { id = genre.Id }, genre);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var genre = await _genreService.UpdateGenreAsync(id, requestDto);
        if (genre == null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var result = await _genreService.DeleteGenreAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}