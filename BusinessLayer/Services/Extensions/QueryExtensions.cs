using BusinessLayer.Models.Common;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Extensions;

public static class QueryExtensions
{
    private const int DefaultLimit = 20;
    private const int MaxLimit = 100;


    private static (int Limit, int Offset) NormalizePaging(int limit, int offset)
    {
        if (limit <= 0)
            limit = DefaultLimit;

        if (limit > MaxLimit)
            limit = MaxLimit;

        if (offset < 0)
            offset = 0;

        return (limit, offset);
    }

    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int limit, int offset)
        => query.Skip(offset).Take(limit);

    public static async Task<PagedResultDto<TOut>> ToPagedResultAsync<TEntity, TOut>(
        this IQueryable<TEntity> query,
        int limit,
        int offset,
        Func<IEnumerable<TEntity>, IEnumerable<TOut>> mapper,
        CancellationToken ct = default
    ) where TEntity : class
    {
        var (normalizedLimit, normalizedOffset) = NormalizePaging(limit, offset);
        var total = await query.CountAsync(ct);

        var entities = await query
            .ApplyPaging(normalizedLimit, normalizedOffset)
            .ToListAsync(ct);

        return new PagedResultDto<TOut>
        {
            Total = total,
            Limit = normalizedLimit,
            Offset = normalizedOffset,
            Items = mapper(entities)
        };
    }
    
    public static IQueryable<Book> FilterBySearchTerm(this IQueryable<Book> query, string? searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
            return query;

        var lowerSearchTerm = searchTerm.Trim().ToLower();

        return query.Where(b =>
            b.Title.ToLower().Contains(lowerSearchTerm) ||
            b.Authors.Any(a => 
                (a.Name.ToLower() + " " + a.Surname.ToLower()).Contains(lowerSearchTerm) ||
                a.Name.ToLower().Contains(lowerSearchTerm) ||
                a.Surname.ToLower().Contains(lowerSearchTerm)) ||
            (b.Publisher != null && b.Publisher.Name.ToLower().Contains(lowerSearchTerm)) ||
            (b.PrimaryGenre != null && b.PrimaryGenre.Name.ToLower().Contains(lowerSearchTerm)) ||
            b.Genres.Any(g => g.Name.ToLower().Contains(lowerSearchTerm))
        );
    }
    
    public static IQueryable<Book> FilterByPrice(this IQueryable<Book> query, double? price)
    {
        if (!price.HasValue)
            return query;

        return query.Where(b => Math.Abs(b.Price - price.Value) <= 0.0001);
    }
}

