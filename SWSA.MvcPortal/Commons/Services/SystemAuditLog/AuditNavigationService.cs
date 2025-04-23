using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Commons.Services.SystemAuditLog;

public static class AuditNavigationService
{
    //Follow the controller route and action route
    public static string? GenerateUrl(SystemAuditModule module, string entityId)
    {
        return module switch
        {
            SystemAuditModule.ScheduleJob => $"/scheduler-jobs/{entityId}/overview",
            SystemAuditModule.Company => $"/companies/{entityId}/overview",
            _ => null
        };
    }
}