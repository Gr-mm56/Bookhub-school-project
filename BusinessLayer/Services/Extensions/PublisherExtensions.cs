using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Extensions;

public static class PublisherExtensions
{
    public static IQueryable<Publisher> WithDetailIncludes(this IQueryable<Publisher> query)
        => query
            .Include(p => p.ProfilePhoto)
            .Include(p => p.Books)
            .ThenInclude(b => b.Image);
}