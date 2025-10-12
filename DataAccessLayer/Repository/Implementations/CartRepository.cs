using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;

public class CartRepository : BaseRepository<Cart>
{
    public CartRepository(BookHubDbContext context) : base(context)
    {
    }
}
