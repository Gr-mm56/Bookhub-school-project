using DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class RatingController: ControllerBase
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
}