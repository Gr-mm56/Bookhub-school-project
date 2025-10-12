using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class GenreRepository: BaseRepository<Genre>, IGenreRepository
{
    public GenreRepository(BookHubDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Genre>> SearchGenresAsync(string name)
    {
        return await _context.Genres
            .Where(g => g.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<Genre?> GetGenreWithBooksAsync(int id)
    {
        return await _context.Genres
            .Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Id == id);
    }
}