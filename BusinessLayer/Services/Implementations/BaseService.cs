using BusinessLayer.Models.Common;
using BusinessLayer.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public abstract class BaseService<TContext>(TContext context)
    where TContext : DbContext
{
    protected readonly TContext Context = context;
    
    protected async Task<PagedResultDto<TOut>> PageAsync<TEntity, TOut>(
        IQueryable<TEntity> query,
        int limit,
        int offset,
        Func<IEnumerable<TEntity>, IEnumerable<TOut>> mapper,
        CancellationToken ct = default
    ) where TEntity : class
        => await query.ToPagedResultAsync(limit, offset, mapper, ct);
    
    protected Task<int> SaveAsync(CancellationToken ct = default) => Context.SaveChangesAsync(ct);
}
