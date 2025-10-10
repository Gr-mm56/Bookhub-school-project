using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;
// todo: On Put, updatedAt should be updated, implicitly
[Route("[controller]")]
[ApiController]
public class RatingController : ControllerBase
{
    private readonly BookHubDbContext _context;

    public RatingController(BookHubDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetRatings(
        [FromQuery] int? userId,
        [FromQuery] int? bookId,
        [FromQuery] int? minStars,
        [FromQuery] int? maxStars,
        [FromQuery] int limit = 20,
        [FromQuery] int offset = 0)
    {
        var query = _context.Ratings.Include(r => r.User).Include(r => r.Book).AsQueryable();

        if (userId.HasValue)
        {
            query = query.Where(r => r.UserId == userId.Value);
        }

        if (bookId.HasValue)
        {
            query = query.Where(r => r.BookId == bookId.Value);
        }

        if (minStars.HasValue)
        {
            query = query.Where(r => r.Stars >= minStars.Value);
        }

        if (maxStars.HasValue)
        {
            query = query.Where(r => r.Stars <= maxStars.Value);
        }

        var ratings = await query.Skip(offset).Take(limit).ToListAsync();
        // map it to a new object
        // todo: this will be mapped to DTO (in 2nd Milestone?)
        var result = ratings.Select(r => new
        {
            r.Id, r.Stars, r.UserId, r.BookId, User = new { r.User.Id, r.User.Name },
            Book = new { r.Book.Id, r.Book.Title }
        });
        return Ok(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetRating(int id)
    {
        var rating = await _context.Ratings
            .Include(r => r.User)
            .Include(r => r.Book)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (rating == null)
        {
            return NotFound();
        }

        var result = new
        {
            rating.Id, rating.Stars, rating.UserId, rating.BookId, User = new { rating.User.Id, rating.User.Name },
            Book = new { rating.Book.Id, rating.Book.Title }
        };
        return Ok(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateRating([FromBody] Rating rating)
    {
        if (rating.Stars is < 0 or > 5)
        {
            return BadRequest("Stars must be between 0 and 5.");
        }

        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRating), new { id = rating.Id }, rating);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateRating(
        int id,
        [FromBody] Rating updatedRating)
    {
        if (id != updatedRating.Id)
        {
            return BadRequest("ID mismatch.");
        }

        var existingRating = await _context.Ratings.FindAsync(id);
        if (existingRating == null)
        {
            return NotFound();
        }

        if (updatedRating.Stars is < 0 or > 5)
        {
            return BadRequest("Stars must be between 0 and 5.");
        }

        existingRating.Stars = updatedRating.Stars;
        existingRating.UserId = updatedRating.UserId;
        existingRating.BookId = updatedRating.BookId;
        _context.Ratings.Update(existingRating);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteRating(int id)
    {
        var rating = await _context.Ratings.FindAsync(id);
        if (rating == null)
        {
            return NotFound();
        }
        _context.Ratings.Remove(rating);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}