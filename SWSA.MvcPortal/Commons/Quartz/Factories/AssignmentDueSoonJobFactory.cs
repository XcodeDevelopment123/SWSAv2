using Quartz;
using SWSA.MvcPortal.Commons.Quartz.Jobs;
using SWSA.MvcPortal.Commons.Quartz.Support;
namespace SWSA.MvcPortal.Commons.Quartz.Factories;

public class AssignmentDueSoonJobFactory : BaseJobFactory
{
    public override JobKey GetJobKey() => QuartzJobKeys.AssignmentDueSoonJobKey;
    public override Type GetJobType() => typeof(AssignmentDueSoonJob);


    //public override ITrigger CreateTrigger(JobBuildContext ctx)
    //{
    //    var builder = TriggerBuilder.Create()
    //              .WithIdentity($"trigger_{Guid.NewGuid()}", ctx.TriggerGroup)
    //              .ForJob(ctx.JobKey);

    //    return builder.StartNow().Build();
    //}
}
