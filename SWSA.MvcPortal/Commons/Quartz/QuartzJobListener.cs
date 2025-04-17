using Quartz;
using Serilog;

namespace SWSA.MvcPortal.Commons.Quartz;


public class QuartzJobListener : IJobListener
{
    public string Name => "GlobalJobListener";

    public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        Log.Warning("⚠️ Job vetoed: {JobKey}, Trigger: {TriggerKey}",
            context.JobDetail.Key, context.Trigger.Key);
        return Task.CompletedTask;
    }

    public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        Log.Information("▶️ Job starting: {JobKey}, Trigger: {TriggerKey}, Scheduled at: {ScheduledTime}, FireTime: {FireTimeUtc}",
            context.JobDetail.Key,
            context.Trigger.Key,
            context.ScheduledFireTimeUtc?.ToLocalTime(),
            context.FireTimeUtc.ToLocalTime());

        return Task.CompletedTask;
    }

    public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
    {
        if (jobException != null)
        {
            Log.Error(jobException,
                "❌ Job failed: {JobKey}, Trigger: {TriggerKey}, Duration: {Duration}ms",
                context.JobDetail.Key,
                context.Trigger.Key,
                context.JobRunTime.TotalMilliseconds);
        }
        else
        {
            Log.Information("✅ Job completed: {JobKey}, Trigger: {TriggerKey}, Duration: {Duration}ms",
                context.JobDetail.Key,
                context.Trigger.Key,
                context.JobRunTime.TotalMilliseconds);
        }

        return Task.CompletedTask;
    }
}