using DataAccessLayer.Context;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository;

public class ImageRepository : BaseRepository<Image>
{
    public ImageRepository(BookHubDbContext context) : base(context)
    {
    }
}