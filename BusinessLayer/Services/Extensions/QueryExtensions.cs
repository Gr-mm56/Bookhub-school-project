using BusinessLayer.Models.Common;
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
}



