using Quartz;
using SWSA.MvcPortal.Commons.Extensions;
using SWSA.MvcPortal.Commons.Quartz.Requests;

namespace SWSA.MvcPortal.Commons.Quartz.Support;

public static class JobDataMapExtensions
{
    public static void AddGenerateReportRequest(this JobDataMap map, GenerateReportJobRequest request)
    {
        map.Put("CompanyId", request.CompanyId);
        map.Put("Month", request.Month.GetIntValue());
        map.Put("Year", request.Year);
    }
}
