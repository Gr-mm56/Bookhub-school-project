using DataAccessLayer.Context;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository;

public class PublisherRepository : BaseRepository<Publisher>
{
    public PublisherRepository(BookHubDbContext context) : base(context)
    {
    }
}