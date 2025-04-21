using Quartz;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Commons.Quartz.Requests;

public interface IJobRequest { }

public class BaseJobRequest : IJobRequest 
{
    public bool IsCustom { get; set; } = true; // User scheduler
    public bool IsSystemJob => !IsCustom;     //Syste mscheduler
    public string? CronExpression { get; set; } 
    public DateTime? StartTime { get; set; }

    // Injected internally for consistency
    public JobKey? ResolvedJobKey { get; set; }
}

public class GenerateReportJobRequest : BaseJobRequest
{
    public int CompanyId { get; set; }
    public MonthOfYear Month { get; set; }
    public int Year { get; set; }
}
