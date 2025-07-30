using CronExpressionDescriptor;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Extensions;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using System.Text.RegularExpressions;

namespace SWSA.MvcPortal.Models.ScheduledJobs;

public class ScheduledJobVM
{
    public string JobKey { get; set; } = default!;
    public ScheduledJobType JobType { get; set; }
    public ScheduleType ScheduleType { get; set; } = default!;
    public DateTime? TriggerTime { get; set; }
    public string? CronExpression { get; set; }
    public bool IsEnabled { get; set; } = true;
    public string CreatedBy { get; set; } = "System Default";
    public string? StaffId { get; set; }
    public DateTime? LastExecuteAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public CronFields CronFields { get; set; }
    public bool IsRequireExtraParams { get; set; }
    public string? PayloadJson { get; set; }
    public string ToReadableSchedule()
    {
        try
        {
            if (ScheduleType == ScheduleType.Once && TriggerTime != null)
            {
                return TriggerTime.ToFormattedString(DateTimeFormatType.DateTime12H);
            }

            if (string.IsNullOrEmpty(CronExpression))
            {
                return AppSettings.NotAvailable;
            }

            var desc = ExpressionDescriptor.GetDescription(CronExpression, new Options
            {
                Locale = "en",
                Use24HourTimeFormat = false,
                Verbose = true
            });

            // 检测 daily / weekly / monthly / specific day 等模式
            if (desc.Contains("every day", StringComparison.OrdinalIgnoreCase))
            {
                var time = ExtractTime(desc);
                return $"Every day at {time}";
            }
            else if (desc.Contains("every", StringComparison.OrdinalIgnoreCase) &&
                     desc.Contains("at", StringComparison.OrdinalIgnoreCase))
            {
                // 可能是 “Every Monday at 10:00 AM”
                return CapitalizeFirst(desc);
            }
            else if (desc.Contains("on day", StringComparison.OrdinalIgnoreCase))
            {
                // e.g. "At 9:00 AM, on day 1 of the month"
                return desc.Replace("At", "at");
            }
            else
            {
                // fallback
                return CapitalizeFirst(desc);
            }
        }
        catch
        {
            return CronExpression ?? "Invalid schedule";
        }
    }

    public CronFields ParseCron()
    {
        if (string.IsNullOrEmpty(CronExpression))
            return new CronFields();

        var parts = CronExpression.Split(' ');
        if (parts.Length < 6) return new CronFields(); // 无效 Cron

        string seconds = parts[0];
        string minutes = parts[1];
        string hours = parts[2];
        string dayOfMonth = parts[3];
        string month = parts[4];
        string dayOfWeek = parts[5];

        var result = new CronFields();

        // 1. Time (HH:mm)
        if (hours != "*" && minutes != "*")
            result.Time = $"{hours.PadLeft(2, '0')}:{minutes.PadLeft(2, '0')}";

        // 2. Day of Week (0–6 or MON–SUN)
        if (dayOfWeek != "?" && dayOfWeek != "*")
        {
            if (int.TryParse(dayOfWeek, out int dayNum))
            {
                result.DayOfWeek = (DayOfWeek?)dayNum;
            }
        }

        // 3. Date (Day of month)
        if (dayOfMonth != "?" && dayOfMonth != "*")
            result.DayOfMonth = dayOfMonth;

        return result;
    }
    private string ExtractTime(string desc)
    {
        var match = Regex.Match(desc, @"at ([0-9]{1,2}:[0-9]{2} ?[AP]M)", RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value : desc;
    }

    private string CapitalizeFirst(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return char.ToUpper(input[0]) + input.Substring(1);
    }

}

public static class ScheduledJobVMExtensions
{
    public static T? GetJobRequest<T>(this ScheduledJobVM job) where T : BaseJobRequest, new()
    {
        if (string.IsNullOrWhiteSpace(job.PayloadJson))
            return null;

        try
        {
            return JsonConvert.DeserializeObject<T>(job.PayloadJson);
        }
        catch
        {
            return null; // 可加日志记录
        }
    }
}