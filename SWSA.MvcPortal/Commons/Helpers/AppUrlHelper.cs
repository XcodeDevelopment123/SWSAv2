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
}
