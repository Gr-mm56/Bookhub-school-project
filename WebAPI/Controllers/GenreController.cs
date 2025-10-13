using BusinessLayer.Models.Common;
using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class GenreController(IGenreService genreService) : Controller
{
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetGenres([FromQuery] PagedRequestDto pagedRequest)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await genreService.GetGenresAsync(pagedRequest.Limit, pagedRequest.Offset);
        return Ok(result);
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> SearchGenres([FromQuery] GenreSearchDto searchDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await genreService.SearchGenresAsync(searchDto);
        return Ok(result);
    }

    [HttpGet]
    [Route("details/{id:int}")]
    public async Task<IActionResult> GetGenreWithBooks(int id)
    {
        var genre = await genreService.GetGenreWithBooksAsync(id);
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
        var genre = await genreService.GetGenreByIdAsync(id);
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

        var genre = await genreService.CreateGenreAsync(requestDto);
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

        var genre = await genreService.UpdateGenreAsync(id, requestDto);
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
        var result = await genreService.DeleteGenreAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}