using Quartz;

namespace SWSA.MvcPortal.Commons.Quartz;

public class QuartzJobListener : IJobListener
{
    public string Name => "GlobalJobListener";
    public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"⚠️ Job vetoed: {context.JobDetail.Key}");
        return Task.CompletedTask;
    }
    public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"▶️ Job executing: {context.JobDetail.Key}");
        return Task.CompletedTask;
    }

    public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
    {
        if (jobException != null)
        {
            Console.WriteLine($"❌ Job failed: {context.JobDetail.Key} - {jobException.Message}");
        }
        else
        {
            Console.WriteLine($"✅ Job completed: {context.JobDetail.Key}");
        }
        return Task.CompletedTask;
    }

}
