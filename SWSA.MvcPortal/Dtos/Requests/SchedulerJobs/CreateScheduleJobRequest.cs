using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Models.ScheduledJobs;

namespace SWSA.MvcPortal.Dtos.Requests.SchedulerJobs;

public class CreateScheduleJobRequest
{
    public ScheduledJobType JobType { get; set; }
    public ScheduleType ScheduleType { get; set; } = default!;
    public CronFields CronFields { get; set; }
    public string CronExpression { get; set; }
    public DateTime? TriggerTime { get; set; }
    public string? PayloadJson { get; set; }

}
