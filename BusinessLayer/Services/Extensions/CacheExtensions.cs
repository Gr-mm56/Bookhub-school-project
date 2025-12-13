using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Services.Extensions;

public static class CacheExtensions
{
    public static async Task<T> GetOrCreateAsync<T>(
        this IMemoryCache cache,
        string cacheKey,
        TimeSpan cacheExpiration,
        Func<Task<T>> getValue
    )
    {
        if (cache.TryGetValue(cacheKey, out T? cachedValue))
        {
            Console.WriteLine($"Cache hit for key: {cacheKey}");
            return cachedValue!;
        }

        var value = await getValue();

        if (value == null) return value!;
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(cacheExpiration);

        cache.Set(cacheKey, value, cacheOptions);

        return value;
    }

    // essentially clears all entries from the memory cache
    // not the most efficient way, unless I want to track all keys
    public static void InvalidateAllCache(this IMemoryCache cache)
    {
        if (cache is MemoryCache memoryCache)
        {
            memoryCache.Compact(1.0);
        }
    }
}

