using Microsoft.Extensions.Caching.Memory;

namespace SWSA.MvcPortal.Commons.Extensions;

public static class CacheExtensions
{
    public static void ApplyOptions(this ICacheEntry entry, MemoryCacheEntryOptions options)
    {
        entry.AbsoluteExpiration = options.AbsoluteExpiration;
        entry.AbsoluteExpirationRelativeToNow = options.AbsoluteExpirationRelativeToNow;
        entry.SlidingExpiration = options.SlidingExpiration;
        entry.Priority = options.Priority;
    }
}
