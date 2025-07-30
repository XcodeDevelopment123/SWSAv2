using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.ClientWorks;

public class ScheduleWorkRequest
{
    public int Year { get; set; }
    public int ClientId { get; set; }
    public List<ScheduleWorkEngagementRequest> Engagements = [];
}

public class ScheduleWorkEngagementRequest
{
    public string PICId { get; set; } //Staff Id
    public DateTime TargetedToStart { get; set; }
    public string? Remarks { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public WorkPriority Priority { get; set; } = WorkPriority.None;
    public List<ReminderRequest> Reminders = [];
}

public class ReminderRequest
{
    public int DayBefore { get; set; } //Day before the target date to send reminder
    public string Label { get; set; } //Title
    public ReminderChannel Method { get; set; } //Email, SMS, etc.   
    public ReminderType Type { get; set; } //Type of reminder, e.g., Meeting Reminder, Document Collection, etc.
}