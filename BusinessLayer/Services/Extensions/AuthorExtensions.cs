using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Extensions;

public static class AuthorExtensions
{
    public static IQueryable<Author> WithFullDetails(this IQueryable<Author> query)
        => query
            .Include(a => a.ProfilePhoto)
            .Include(a => a.Books)
            .ThenInclude(b => b.Image);

    public static IQueryable<Author> WithDetailIncludes(this IQueryable<Author> query)
        => query.WithFullDetails();

    public static IQueryable<Author> WithDeleteIncludes(this IQueryable<Author> query)
        => query
            .Include(a => a.Books)
            .ThenInclude(b => b.Authors)
            .Include(a => a.Books)
            .ThenInclude(b => b.Ratings)
            .Include(a => a.Books)
            .ThenInclude(b => b.Genres);
}