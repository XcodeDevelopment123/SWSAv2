using Quartz;
using SWSA.MvcPortal.Commons.Constants;

namespace SWSA.MvcPortal.Commons.Quartz.Support;

public class QuartzJobKeys
{
    public static readonly JobKey AssignmentDueSoonJobKey = new JobKey("assignmentDueSoonJob", QuartzGroupKeys.NotificationGroup);
    public static readonly JobKey GenerateAssignmentReportJobKey = new JobKey("generateAssignmentReportJob", QuartzGroupKeys.ReportGroup);

}

public enum QuratzJobType
{
    //Notification group
    AssignmentDueSoon = 100,

    //Report group

    /// <summary>
    /// Required use GenerateReportJobRequest as IJobRequest
    /// </summary>
    GenerateAssignmentReport = 200

    //Other 
}
