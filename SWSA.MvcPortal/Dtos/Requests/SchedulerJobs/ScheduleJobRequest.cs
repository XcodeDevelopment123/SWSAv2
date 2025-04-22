using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Models.ScheduledJobs;

namespace SWSA.MvcPortal.Dtos.Requests.SchedulerJobs;

public class ScheduleJobRequest
{
    public string JobKey { get; set; } = default!;
    public ScheduleType ScheduleType { get; set; } = default!;
    public CronFields CronFields { get; set; }
    public string CronExpression { get; set; }
    public DateTime? TriggerTime { get; set; }
    public bool IsEnabled { get; set; } = true;
    public bool ExecuteNow { get; set; } = false;
}
