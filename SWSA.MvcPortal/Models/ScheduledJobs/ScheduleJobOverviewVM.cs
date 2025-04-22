using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.ScheduledJobs;

public class ScheduleJobOverviewVM
{
    public ScheduledJobType JobType { get; set; }
    public string DisplayName { get; set; } = "";
    public string Description { get; set; } = "";

    public string RequestSchema { get; set; } = ""; // e.g. JSON Schema for dynamic form
    public string? SettingSchema { get; set; }      // optional
}
