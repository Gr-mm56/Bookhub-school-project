using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Extensions;


public static class GenreExtensions
{

    public static IQueryable<Genre> WithDetailIncludes(this IQueryable<Genre> query)
        => query
            .Include(g => g.Books)
            .Include(g => g.PrimaryBooks);
}

