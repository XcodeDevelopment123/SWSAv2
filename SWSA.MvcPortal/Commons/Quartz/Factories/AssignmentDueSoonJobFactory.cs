using Quartz;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Commons.Quartz.Jobs;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using SWSA.MvcPortal.Commons.Quartz.Support;

namespace SWSA.MvcPortal.Commons.Quartz.Factories;

public class AssignmentDueSoonJobFactory : BaseJobFactory
{
    public override IJobDetail CreateJob(IJobRequest? request)
    {
        return JobBuilder.Create<AssignmentDueSoonJob>()
            .WithIdentity(QuartzJobKeys.AssignmentDueSoonJobKey)
            .Build();
    }

    public override ITrigger CreateTrigger(IJobRequest? request)
    {
        return TriggerBuilder.Create()
            .WithIdentity($"trigger_{Guid.NewGuid()}", QuartzGroupKeys.NotificationGroup)
            .WithCronSchedule(CronExpressionBuilder.DailyAt(9)) // every day 9:00 AM
            .ForJob(QuartzJobKeys.AssignmentDueSoonJobKey)
            .Build();
    }
}
