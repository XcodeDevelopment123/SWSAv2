using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Commons.Quartz.Requests;

public interface IJobRequest { }

public class GenerateReportJobRequest : IJobRequest
{
    public int CompanyId { get; set; }
    public MonthOfYear Month { get; set; }
    public int Year { get; set; }
}
