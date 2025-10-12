using DataAccessLayer.Context;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository;

public class AuthorRepository : BaseRepository<Author>
{
    public AuthorRepository(BookHubDbContext context) : base(context)
    {
    }
}