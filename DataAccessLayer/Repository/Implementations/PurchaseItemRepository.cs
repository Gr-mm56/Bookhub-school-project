using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;

public class PurchaseItemRepository : BaseRepository<PurchaseItem>
{
    public PurchaseItemRepository(BookHubDbContext context) : base(context)
    {
    }
}
