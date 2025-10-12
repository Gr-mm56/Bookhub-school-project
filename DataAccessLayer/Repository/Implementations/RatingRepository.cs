using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class RatingRepository: BaseRepository<Rating>, IRatingRepository
{
   public RatingRepository(BookHubDbContext context) : base(context)
   {
   }
   public async Task<List<Rating>> GetRatingsAsync(
      int? userId,
      int? bookId,
      int? minStars,
      int? maxStars,
      int limit,
      int offset)
   {
      IQueryable<Rating> query = _context.Ratings
         .Include(r => r.User)
         .Include(r => r.Book);

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

      return await query
         .Skip(offset)
         .Take(limit)
         .ToListAsync();
   }
}