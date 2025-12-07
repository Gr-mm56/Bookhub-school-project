using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Extensions;


public static class PurchaseItemExtensions
{
    
    public static IQueryable<PurchaseItem> WithDetailIncludes(this IQueryable<PurchaseItem> query)
        => query
            .Include(p => p.Book)
            .Include(p => p.Cart);
}

