using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;

public class WishlistItemRepository : BaseRepository<WishlistItem>
{
    public WishlistItemRepository(BookHubDbContext context) : base(context)
    {
    }
}
