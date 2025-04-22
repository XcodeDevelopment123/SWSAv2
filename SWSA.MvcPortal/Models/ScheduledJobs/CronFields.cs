namespace SWSA.MvcPortal.Models.ScheduledJobs;

public class CronFields
{
    public DayOfWeek? DayOfWeek { get; set; }   
    public string? DayOfMonth { get; set; }   // 1-31
    public string? Time { get; set; }         // HH:mm 格式
}
