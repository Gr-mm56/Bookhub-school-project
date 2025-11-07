using BusinessLayer.Models.Genre.Requests;
using BusinessLayer.Models.Genre.Responses;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class GenreController : BaseController<GenreDto, GenreDetailDto, GenreRequestDto, GenreRequestDto, IGenreService>
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
}
