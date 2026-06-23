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
}

