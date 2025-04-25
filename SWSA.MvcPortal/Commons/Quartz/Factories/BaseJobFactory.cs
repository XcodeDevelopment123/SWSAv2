using Quartz;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using SWSA.MvcPortal.Commons.Quartz.Support;
namespace SWSA.MvcPortal.Commons.Quartz.Factories;

public interface IJobBaseFactory
{
    IJobDetail CreateJob(JobBuildContext context);
    ITrigger CreateTrigger(JobBuildContext context);
    JobKey GetJobKey();
}

/// <summary>
/// You will no need to change this base job factory, except you know what need to change and correct
/// Method CreateTrigger, could be override by child job class
/// Every new inherit job class should have own key, use the method GetJobKey()
///   example 
///   public override JobKey GetJobKey() => QuartzJobKeys.GenerateAssignmentReportJobKey;
/// </summary>
public abstract class BaseJobFactory : IJobBaseFactory
{
    public abstract JobKey GetJobKey();
    public abstract Type GetJobType();

    public virtual IJobDetail CreateJob(JobBuildContext ctx)
    {
        var builder = JobBuilder.Create(GetJobType())
            .WithIdentity(ctx.JobKey)
            .StoreDurably(!IsOneTimeJob(ctx));

        if (ctx.Request != null)
            builder = builder.UsingJobData(JobDataBinder.ToJobDataMap(ctx.Request));

        return builder.Build();
    }

    public virtual ITrigger CreateTrigger(JobBuildContext ctx)
    {
        var builder = TriggerBuilder.Create()
            .WithIdentity($"trigger_{Guid.NewGuid()}", ctx.TriggerGroup)
            .ForJob(ctx.JobKey);

        if (ctx.Request is BaseJobRequest r && r.StartTime.HasValue)
            return builder.StartAt(r.StartTime.Value).WithSimpleSchedule(x => x.WithRepeatCount(0)).Build();

        if (!string.IsNullOrWhiteSpace((ctx.Request as BaseJobRequest)?.CronExpression))
            return builder.WithCronSchedule(((BaseJobRequest)ctx.Request)!.CronExpression!).Build();

        return builder.WithCronSchedule(CronExpressionBuilder.DailyAt("9:00")).Build();
    }

    private bool IsOneTimeJob(JobBuildContext ctx)
        => ctx.Request is BaseJobRequest r && r.StartTime.HasValue;
}
