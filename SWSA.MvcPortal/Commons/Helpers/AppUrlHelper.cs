using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Commons.Helpers;

public static class AppUrlHelper
{

    private static readonly Dictionary<WorkType, string> WorkTypeEditRoutes = new()
    {
        [WorkType.AnnualReturn] = "companies/works/annual-return/{taskId}/edit",
        [WorkType.Audit] = "companies/works/audit/{taskId}/edit",
        [WorkType.StrikeOff] = "companies/works/strike-off/{taskId}/edit",
        [WorkType.LLP] = "companies/works/llp/{taskId}/edit"
    };

    public static string GenerateWorkEditUrl(WorkType type, int taskId)
    {
        return "/" + WorkTypeEditRoutes[type].Replace("{taskId}", taskId.ToString());
    }

    public static string? GenerateAuditUrl(SystemAuditModule module, string entityId)
    {
        return module switch
        {
            SystemAuditModule.ScheduleJob => $"/scheduler-jobs/{entityId}/overview",
            SystemAuditModule.Company => $"/companies/{entityId}/overview",
            SystemAuditModule.CompanyComplianceDate => $"/companies/{entityId}/overview",
            SystemAuditModule.CompanyWorkAssignment => $"/companies/works/{entityId}/edit",
            SystemAuditModule.DocumentRecord => $"/companies/docs?docId={entityId}",
            SystemAuditModule.User => $"/users/{entityId}/overview",

            SystemAuditModule.Client => $"/clients/{entityId}/navigate",
            SystemAuditModule.WorkAllocation => $"/clients/{entityId}/navigate?target=workAllocation",
            SystemAuditModule.OfficialContact => $"/clients/{entityId}/navigate?target=officialContact",
            SystemAuditModule.CompanyOwner => $"/clients/{entityId}/navigate?target=cpOwner",
            SystemAuditModule.CommunicationContact => $"/clients/{entityId}/navigate?target=commContact",
            _ => null
        };
    }
}
