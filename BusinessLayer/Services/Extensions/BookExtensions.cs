using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Extensions;

public static class BookExtensions
{
    public static IQueryable<Book> WithListIncludes(this IQueryable<Book> query)
        => query.Include(b => b.Image);
    
    public static IQueryable<Book> WithDetailIncludes(this IQueryable<Book> query)
        => query
            .Include(b => b.Image)
            .Include(b => b.PrimaryGenre)
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .Include(b => b.Publisher)
            .Include(b => b.Ratings);

    public static IQueryable<Book> WithSearchIncludes(this IQueryable<Book> query)
        => query
            .Include(b => b.Image)
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .Include(b => b.Publisher)
            .Include(b => b.PrimaryGenre);

    public static IQueryable<Book> WithEditIncludes(this IQueryable<Book> query)
        => query
            .Include(b => b.Image)
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .Include(b => b.Publisher);

    public static IQueryable<Book> WithBaseIncludes(this IQueryable<Book> query)
        => query.WithEditIncludes();
}

