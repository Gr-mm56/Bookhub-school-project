using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface IRatingRepository : IRepository<Rating>
{
    Task<List<Rating>> GetRatingsAsync(
        int? userId,
        int? bookId,
        int? minStars,
        int? maxStars,
        int limit,
        int offset);
}