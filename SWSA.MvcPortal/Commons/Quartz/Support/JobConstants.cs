using Quartz;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Commons.Quartz.Support;

/// <summary>
///  When you have create new job, u must define the key to the QuartzJobKeys const value
/// </summary>
public class QuartzJobKeys
{
    public static readonly JobKey AssignmentDueSoonJobKey = new JobKey(nameof(ScheduledJobType.AssignmentDueSoon), QuartzGroupKeys.NotificationGroup);
    public static readonly JobKey GenerateAssignmentReportJobKey = new JobKey(nameof(ScheduledJobType.GenerateAssignmentReport), QuartzGroupKeys.ReportGroup);
}

