using DataAccessLayer.Context;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository;

public class GenreRepository: BaseRepository<Genre>
{
    public GenreRepository(BookHubDbContext context) : base(context)
    {
    }
}