using SWSA.MvcPortal.Commons.Attributes;

namespace SWSA.MvcPortal.Commons.Enums;


public enum ScheduledJobType
{
    //Notification group
    AssignmentDueSoon = 100,
    AssignmentRemind = 101,

    //Report group

    /// <summary>
    /// Required use GenerateReportJobRequest as IJobRequest
    /// </summary>
    [EnumIgnore]
    GenerateAssignmentReport = 200

    //Other 
}
