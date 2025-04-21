using Quartz;
using SWSA.MvcPortal.Commons.Quartz.Jobs;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using SWSA.MvcPortal.Commons.Quartz.Support;

namespace SWSA.MvcPortal.Commons.Quartz.Factories;

public class GenerateAssignmentReportJobFactory : BaseJobFactory
{
    public override JobKey GetJobKey() => QuartzJobKeys.GenerateAssignmentReportJobKey;
    public override Type GetJobType() => typeof(GenerateAssignmentReportJob);

    /// <summary>
    /// 任务需要参数，因此必须 override CreateJob，并注入 JobDataMap
    /// </summary>
    public override IJobDetail CreateJob(JobBuildContext ctx)
    {
        if (ctx.Request is not GenerateReportJobRequest reportReq)
            throw new ArgumentException("Invalid request type for GenerateAssignmentReportJob");

        var jobBuilder = JobBuilder.Create<GenerateAssignmentReportJob>()
            .WithIdentity(ctx.JobKey)
            .StoreDurably(!reportReq.StartTime.HasValue)
            .UsingJobData(JobDataBinder.ToJobDataMap(reportReq));

        return jobBuilder.Build();
    }

    /// <summary>
    /// 如需自定义触发逻辑（例如立即执行），可以选择性 override
    /// 默认行为已支持 StartTime / CronExpression
    /// </summary>
    // public override ITrigger CreateTrigger(JobBuildContext ctx)
    // {
    //     // 可选 override，仅当默认逻辑不满足需求
    //     return base.CreateTrigger(ctx);
    // }
}