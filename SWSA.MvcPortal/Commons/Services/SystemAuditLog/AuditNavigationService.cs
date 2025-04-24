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
            SystemAuditModule.CompanyComplianceDate => $"/companies/{entityId}/overview",
            SystemAuditModule.CompanyOfficialContact => $"/companies/{entityId}/overview",
            SystemAuditModule.CompanyOwner => $"/companies/{entityId}/overview",
            SystemAuditModule.CompanyStaff => $"/companies/staffs/{entityId}/edit",//Should be overview
            SystemAuditModule.CompanyWorkAssignment => $"/companies/works/{entityId}/edit", //Should be overview
            SystemAuditModule.DocumentRecord => $"/companies/docs?docId={entityId}",
            SystemAuditModule.User => $"/users/{entityId}/overview",
            _ => null
        };
    }
}