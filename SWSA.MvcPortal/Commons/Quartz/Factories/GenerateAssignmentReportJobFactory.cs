using Quartz;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Quartz.Jobs;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using SWSA.MvcPortal.Commons.Quartz.Support;

namespace SWSA.MvcPortal.Commons.Quartz.Factories;

public class GenerateAssignmentReportJobFactory : BaseJobFactory
{
    public override IJobDetail CreateJob(IJobRequest? request)
    {
        var cast = request as GenerateReportJobRequest ?? throw new ArgumentException("Invalid request type.");

        var map = new JobDataMap();
        map.AddGenerateReportRequest(cast);

        return JobBuilder.Create<GenerateAssignmentReportJob>()
            .WithIdentity(QuartzJobKeys.GenerateAssignmentReportJobKey)
            .UsingJobData(map)
            .Build();
    }

    public override ITrigger CreateTrigger(IJobRequest? request)
    {
        return TriggerBuilder.Create()
            .WithIdentity($"trigger_{Guid.NewGuid()}", QuartzGroupKeys.ReportGroup)
            .StartNow() // If need generate automatic every month, change it to repeat and apply Cron, and add background job at scheduler background service
            .ForJob(QuartzJobKeys.GenerateAssignmentReportJobKey)
            .Build();

        //return TriggerBuilder.Create()
        //.WithIdentity($"trigger_{Guid.NewGuid()}", QuartzGroupKeys.ReportGroup)
        //.WithCronSchedule(CronExpressionBuilder.MonthlyAt(1, 9, 0)) // every month 1th 9:00 AM
        //.ForJob(JobKeys.GenerateAssignmentReportJobKey)
        //.Build();
    }
}