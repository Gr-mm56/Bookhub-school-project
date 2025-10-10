using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class GenreController(BookHubDbContext context) : ControllerBase
{

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetGenres()
    {
        var genres = await context.Genres.ToListAsync();
        var result = new { Genres = genres.Select(g => new { g.Id, g.Name }) };
        return Ok(result);
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> SearchGenres([FromQuery] string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("Name query parameter is required.");
        }
        var genres = await context.Genres
            .Where(g => g.Name.Contains(name))
            .ToListAsync();
        var result = new { Genres = genres.Select(g => new { g.Id, g.Name }) };
        return Ok(result);
    }
    
    [HttpGet]
    [Route("details/{id:int}")]
    public async Task<IActionResult> GetGenreWithBooks(int id)
    {
        var genre = await context.Genres
            .Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null)
        {
            return NotFound();
        }

        return Ok(new
        {
            GenreId = genre.Id,
            GenreName = genre.Name,
            Books = genre.Books?.Select(b => new
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
    public async Task<IActionResult> GetGenreById(int id)
    {
        var genre = await context.Genres
            .FirstOrDefaultAsync(g => g.Id == id);
        if (genre == null)
        {
            return NotFound();
        }
        var result = new { genre.Id, genre.Name };
        return Ok(result);
    }
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateGenre([FromBody] Genre newGenre)
    {
        if (newGenre == null || string.IsNullOrEmpty(newGenre.Name))
        {
            return BadRequest("Genre name is required.");
        }
        // todo: get rid of this after adding DTOs
        var existingGenre = await context.Genres.AnyAsync(g => g.Name == newGenre.Name || g.Id == newGenre.Id);
        if (existingGenre)
        {
            return Conflict();
        }
        context.Genres.Add(newGenre);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetGenreById), new { id = newGenre.Id }, new { newGenre.Id, newGenre.Name });
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] Genre updatedGenre)
    {
        if (id != updatedGenre.Id)
        {
            return BadRequest("ID in URL does not match ID in body.");
        }

        var existingGenre = await context.Genres.FindAsync(id);
        if (existingGenre == null)
        {
            return NotFound();
        }

        existingGenre.Name = updatedGenre.Name;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var deleteGenre = await context.Genres.FindAsync(id);
        if (deleteGenre == null)
        {
            return NotFound();
        }

        context.Genres.Remove(deleteGenre);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
