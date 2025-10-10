using DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class GenreController(BookHubDbContext context) : ControllerBase
{
    private readonly BookHubDbContext _context = context;

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetGenres()
    {
        var genres = await _context.Genres.ToListAsync();
        return Ok(genres);
    }
    [HttpGet]
    [Route("details/{id:int}")]
    public async Task<IActionResult> GetGenreWithBooks(int id)
    {
        var genre = await _context.Genres
            .Include(g => g.Books) // Assuming a navigation property 'Books' exists in Genre
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
        var genre = await _context.Genres
            .FirstOrDefaultAsync(g => g.Id == id);
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
        var deleteGenre = await _context.Genres.FindAsync(id);
        if (deleteGenre == null)
        {
            return NotFound();
        }

        _context.Genres.Remove(deleteGenre);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
