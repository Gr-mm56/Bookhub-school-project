using DataAccessLayer.Context;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository;

public class RatingRepository: BaseRepository<Rating>
{
   public RatingRepository(BookHubDbContext context) : base(context)
   {
   }
}