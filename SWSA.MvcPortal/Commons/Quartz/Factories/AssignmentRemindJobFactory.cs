using Quartz;
using SWSA.MvcPortal.Commons.Quartz.Jobs;
using SWSA.MvcPortal.Commons.Quartz.Support;

namespace SWSA.MvcPortal.Commons.Quartz.Factories;

public class AssignmentRemindJobFactory : BaseJobFactory
{
    public override JobKey GetJobKey() => QuartzJobKeys.AssignmentRemindJobKey;
    public override Type GetJobType() => typeof(AssignmentRemindJob);


    //public override ITrigger CreateTrigger(JobBuildContext ctx)
    //{
    //    var builder = TriggerBuilder.Create()
    //              .WithIdentity($"trigger_{Guid.NewGuid()}", ctx.TriggerGroup)
    //              .ForJob(ctx.JobKey);

    //    return builder.StartNow().Build();
    //}
}