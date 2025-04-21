using CronExpressionDescriptor;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Extensions;
using System.Text.RegularExpressions;

namespace SWSA.MvcPortal.Models.ScheduledJobs;

public class ScheduledJobVM
{
    public string JobKey { get; set; } = default!;
    public ScheduleType ScheduleType { get; set; } = default!;
    public DateTime? TriggerTime { get; set; }
    public string? CronExpression { get; set; }
    public bool IsEnabled { get; set; } = true;
    public string CreatedBy { get; set; } = "System Default";
    public string? StaffId { get; set; }
    public DateTime? LastUpdatedAt { get; set; }


    public string ToReadableSchedule()
    {
        try
        {
            if (ScheduleType != ScheduleType.Once && TriggerTime != null)
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
