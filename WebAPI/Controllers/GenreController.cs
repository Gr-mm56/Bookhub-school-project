using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Models.Genre.Responses;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class GenreController : BaseController<GenreDto, GenreRequestDto, GenreRequestDto, IGenreService>
{
    public GenreController(IGenreService genreService) : base(genreService)
    {
    }

    [HttpGet]
    [Route("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchGenres([FromQuery] GenreSearchDto searchDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await Service.SearchGenresAsync(searchDto);
        
        return Ok(result);
    }

    [HttpGet]
    [Route("books/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGenreWithBooks(int id)
    {
        var genre = await Service.GetGenreWithBooksAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        return Ok(genre);
    }
}