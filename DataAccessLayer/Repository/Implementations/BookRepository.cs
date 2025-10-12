using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class BookRepository: BaseRepository<Book>, IBookRepository
{
    public BookRepository(BookHubDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Book>> SearchBooksAsync(string? title, string? description, string? author, string? genre, string? publisher, double? price)
    {
        var query = _context.Books.Include(b => b.Genres)
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

        return await query.ToListAsync();
    }
}