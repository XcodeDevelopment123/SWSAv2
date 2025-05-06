namespace SWSA.MvcPortal.Commons.Services.Permission;

public interface IPermissionRefreshTracker
{
    void MarkRefreshNeeded(string staffId);
    bool IsRefreshNeeded(string staffId);
    void Clear(string staffId);
}
