using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(BookHubDbContext context) : base(context)
    {
    }
}
