using Microsoft.Extensions.Caching.Memory;

namespace SWSA.MvcPortal.Commons.Services.Permission;

public class MemoryPermissionRefreshTracker(
    IMemoryCache _cache,
    MemoryCacheEntryOptions _cacheOptions
    ) : IPermissionRefreshTracker
{
    public void MarkRefreshNeeded(string staffId)
    {
        _cache.Set(GetKey(staffId), true, _cacheOptions);
    }

    public bool IsRefreshNeeded(string staffId)
    {
        return _cache.TryGetValue(GetKey(staffId), out _);
    }

    public void Clear(string staffId)
    {
        _cache.Remove(GetKey(staffId));
    }

    private static string GetKey(string staffId) => $"NeedPermissionRefresh:{staffId}";
}
