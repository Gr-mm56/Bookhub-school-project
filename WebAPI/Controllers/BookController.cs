using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[Route("[controller]")]
public class BookController(BookHubDbContext context) : Controller
{
    [HttpGet]
    [Route("")]
    public IActionResult GetBooks()
    {
        var books = context.Books//.Include(b => b.Genres)
         //   .Include(b => b.Authors)
         //   .Include(b => b.Publishers)
          //  .Include(b => b.Image)
         //   .Include(b => b.Ratings)
            .ToListAsync();
        Console.WriteLine("Fetched books: " + books.Result);
        return NotFound(); //Ok(books);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await context.Books
            .Include(b => b.Genres)
            .Include(b => b.Authors)
            .Include(b => b.Publishers)
            .Include(b => b.Image)
            .Include(b => b.Ratings)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
        {
            return NotFound();
        }
        var returnBooks = new { book.Id, book.Title, book.ISBN, book.Description, book.Price, book.CreatedAt, book.ImageId,
            Genres = book.Genres?.Select(g => new { g.Id, g.Name }),
            Authors = book.Authors?.Select(a => new { a.Id, a.Name }),
            Publishers = book.Publishers?.Select(p => new { p.Id, p.Name }),
            Image = book.Image != null ? new { book.Image.Id, book.Image.FileUrl } : null,
            Ratings = book.Ratings?.Select(r => new { r.Id, r.UserId })
        };
        return Ok(returnBooks);
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> SearchBooks([FromQuery] string? title, [FromQuery] string? description,
        [FromQuery] string? author, [FromQuery] string? genre, [FromQuery] string? publisher, [FromQuery] double? price)
    {
        var query = context.Books.AsQueryable();
        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(b => b.Title.Contains(title));
        }

        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(b => b.Authors != null && b.Authors.Any(a => a.Name.Contains(author)));
        }

        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(b => b.Genres.Any(g => g.Name.Contains(genre)));
        }

        if (!string.IsNullOrEmpty(publisher))
        {
            query = query.Where(b => b.Publishers != null && b.Publishers.Any(p => p.Name.Contains(publisher)));
        }

        if (price.HasValue)
        {
            query = query.Where(b => b.Price == price.Value);
        }

        var books = await query
            .Include(b => b.Genres)
            .Include(b => b.Authors)
            .Include(b => b.Publishers)
            .Include(b => b.Image)
            .Include(b => b.Ratings)
            .ToListAsync();

        return Ok(books);
    }
}