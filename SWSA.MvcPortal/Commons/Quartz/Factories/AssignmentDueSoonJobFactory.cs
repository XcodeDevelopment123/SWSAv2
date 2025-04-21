using Quartz;
using SWSA.MvcPortal.Commons.Quartz.Jobs;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using SWSA.MvcPortal.Commons.Quartz.Support;

namespace SWSA.MvcPortal.Commons.Quartz.Factories;


public class AssignmentDueSoonJobFactory : BaseJobFactory
{
    public override JobKey GetJobKey() => QuartzJobKeys.AssignmentDueSoonJobKey;
    public override Type GetJobType() => typeof(AssignmentDueSoonJob);
}
