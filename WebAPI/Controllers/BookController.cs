using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[Route("[controller]")]
public class BookController(BookHubDbContext context) : Controller
{
    [HttpGet]
    [Route("list")]
    public IActionResult GetBooks()
    {
        var books = context.Books.Include(book => book.Authors)
            .Include(b => b.Genres)
            .Include(b => b.Authors)
            .Include(b => b.Publishers)
            .Include(b => b.Image)
            .Include(b => b.Ratings)
            .ToListAsync();
        Console.WriteLine("Fetched books: " + books.Result);
        var result = new
        {
            Books = books.Result.Select(b => new
            {
                b.Id,
                b.Title,
                b.ISBN,
                b.Description,
                b.Price,
                b.CreatedAt,
                b.ImageId,
                Genres = b.Genres?.Select(g => new { g.Id, g.Name }),
                Authors = b.Authors?.Select(a => new { a.Id, a.Name }),
                Publishers = b.Publishers?.Select(p => new { p.Id, p.Name }),
                Image = b.Image != null ? new { b.Image.Id, b.Image.FileUrl } : null,
                Ratings = b.Ratings?.Select(r => new { r.Id, r.UserId })
            })
        };
        return Ok(result);
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

        var returnBooks = new
        {
            book.Id, book.Title, book.ISBN, book.Description, book.Price, book.CreatedAt, book.ImageId,
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
        var query = context.Books.Include(b => b.Genres)
            .Include(b => b.Authors)
            .Include(b => b.Publishers)
            .Include(b => b.Image)
            .Include(b => b.Ratings)
            .AsQueryable();
        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(b => b.Title.Contains(title));
        }

        if (!string.IsNullOrEmpty(author))
        {
           // query = query.Where(b => b.Authors.Any(a => EF.Functions.Like(a.Name, $"%{author}%")));
            query = query.Where(b => b.Authors.Any(a => a.Name.Contains(author)));
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
         /*   .Include(b => b.Genres)
            .Include(b => b.Authors)
            .Include(b => b.Publishers)
            .Include(b => b.Image)
            .Include(b => b.Ratings)*/
            .ToListAsync();
        var result = new
        {
            Books = books.Select(b => new
            {
                b.Id,
                b.Title,
                b.ISBN,
                b.Description,
                b.Price,
                b.CreatedAt,
                b.ImageId,
                Genres = b.Genres?.Select(g => new { g.Id, g.Name }),
                Authors = b.Authors?.Select(a => new { a.Id, a.Name }),
                Publishers = b.Publishers?.Select(p => new { p.Id, p.Name }),
                Image = b.Image != null ? new { b.Image.Id, b.Image.FileUrl } : null,
                Ratings = b.Ratings?.Select(r => new { r.Id, r.UserId })
            })
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] Book newBook)
    {
        if (newBook == null || string.IsNullOrEmpty(newBook.Title) || string.IsNullOrEmpty(newBook.ISBN))
        {
            return BadRequest("Book title and ISBN are required.");
        }

        newBook.CreatedAt = DateTime.UtcNow;
        // todo: remove id check
        var bookExists = await context.Books.AnyAsync(b => b.Id == newBook.Id);
        if (bookExists)
        {
            return Conflict("A book with the same ID already exists.");
        }
        context.Books.Add(newBook);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = await context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        book.Title = updatedBook.Title;
        book.ISBN = updatedBook.ISBN;
        book.Description = updatedBook.Description;
        book.Price = updatedBook.Price;
        book.ImageId = updatedBook.ImageId;
        context.Books.Update(book);
        await context.SaveChangesAsync();
        return Ok(new { message = "Book updated successfully." });
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        context.Books.Remove(book);
        await context.SaveChangesAsync();
        return NoContent();
    }
}