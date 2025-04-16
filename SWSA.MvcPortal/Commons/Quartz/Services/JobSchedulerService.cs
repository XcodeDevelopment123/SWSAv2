using Quartz;
using Quartz.Impl.Matchers;
using SWSA.MvcPortal.Commons.Quartz.Factories;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;
using SWSA.MvcPortal.Commons.Quartz.Support;

namespace SWSA.MvcPortal.Commons.Quartz.Services;

public class JobSchedulerService(
    IScheduler scheduler,
    IServiceProvider serviceProvider
    ) : IJobSchedulerService
{
    public async Task ScheduleJob(IJobRequest? request, QuratzJobType type)
    {
        //Every job are require to register here
        IJobBaseFactory jobFactory = type switch
        {
            QuratzJobType.GenerateAssignmentReport => serviceProvider.GetRequiredService<GenerateAssignmentReportJobFactory>(),
            _ => throw new NotImplementedException("Job type not supported.")
        };

        var job = jobFactory.CreateJob(request);
        var trigger = jobFactory.CreateTrigger(request);
        await scheduler.ScheduleJob(job, trigger);
    }

    public async Task ScheduleBackgroundJob()
    {
        //Only background auto job are required to register here
        Dictionary<JobKey, IJobBaseFactory> jobFactories = new Dictionary<JobKey, IJobBaseFactory>
        {
          { QuartzJobKeys.AssignmentDueSoonJobKey, serviceProvider.GetRequiredService<AssignmentDueSoonJobFactory>() },
            // 如果你有其他作业，可以类似地添加到字典中
        };
        foreach (var jobFactory in jobFactories)
        {
            var jobKey = jobFactory.Key;
            var factory = jobFactory.Value;

            var jobDetail = await scheduler.GetJobDetail(jobKey);
            if (jobDetail == null)
            {
                // 创建 JobDetail 和 Trigger
                var job = factory.CreateJob(null);
                var trigger = factory.CreateTrigger(null);
                // 调度作业
                await scheduler.ScheduleJob(job, trigger);
            }
        }
    }

    public async Task ClearAllJobs()
    {
        await scheduler.Clear();
        Console.WriteLine("All jobs cleared.");
    }

    public async Task ClearJobsByGroup(string groupName)
    {
        var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName));

        foreach (var jobKey in jobKeys)
        {
            await scheduler.DeleteJob(jobKey);
            Console.WriteLine($"Job {jobKey} in group {groupName} deleted.");
        }
    }

    public async Task ClearTriggersByGroup(string groupName)
    {
        var triggerKeys = await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(groupName));

        foreach (var triggerKey in triggerKeys)
        {
            await scheduler.UnscheduleJob(triggerKey);
            Console.WriteLine($"Trigger {triggerKey} in group {groupName} unscheduled.");
        }
    }
}
