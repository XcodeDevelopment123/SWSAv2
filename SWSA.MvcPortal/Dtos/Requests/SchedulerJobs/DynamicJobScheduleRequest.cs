using System.Text.Json;

namespace SWSA.MvcPortal.Dtos.Requests.SchedulerJobs;

public class DynamicJobScheduleRequest
{
    public string JobKey { get; set; } = default!;
    public DateTime? StartTime { get; set; } // One time job
    public string? CronExpression { get; set; } // repeatly job
    public JsonElement? Payload { get; set; }
}
