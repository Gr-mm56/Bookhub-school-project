using BusinessLayer.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public abstract class BaseService<TContext>(TContext context)
    where TContext : DbContext
{
    protected readonly TContext Context = context;

    private static (int Limit, int Offset) NormalizePaging(int limit, int offset, int defaultLimit = 20, int maxLimit = 100)
    {
        if (limit <= 0)
        {
            limit = defaultLimit;
        }

        if (limit > maxLimit)
        {
            limit = maxLimit;
        }

        if (offset < 0)
        {
            offset = 0;
        }
        return (limit, offset);
    }

    private static IQueryable<T> ApplyPaging<T>(IQueryable<T> query, int limit, int offset)
        => query.Skip(offset).Take(limit);

    protected async Task<PagedResultDto<TOut>> PageAsync<TEntity, TOut>(
        IQueryable<TEntity> query,
        int limit,
        int offset,
        Func<IEnumerable<TEntity>, IEnumerable<TOut>> mapper,
        CancellationToken ct = default)
        where TEntity : class
    {
        var (normalizedLimit, normalizedOffset) = NormalizePaging(limit, offset);
        var total = await query.CountAsync(ct);

        var entities = await ApplyPaging(query, normalizedLimit, normalizedOffset)
            .ToListAsync(ct);

        return new PagedResultDto<TOut>
        {
            Total = total,
            Limit = normalizedLimit,
            Offset = normalizedOffset,
            Items = mapper(entities)
        };
    }

    protected Task<int> SaveAsync(CancellationToken ct = default) => Context.SaveChangesAsync(ct);
}
