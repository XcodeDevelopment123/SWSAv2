using Quartz;
using Quartz.Impl.Matchers;
using Serilog;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;
using SWSA.MvcPortal.Commons.Quartz.Support;
using SWSA.MvcPortal.Repositories.Interfaces;

namespace SWSA.MvcPortal.Commons.Quartz.Services;

/// <summary>
/// You will no need to change this scheduler service, except you know what need to change and correct
/// </summary>
public class JobSchedulerService(
    IScheduler scheduler,
    IServiceProvider serviceProvider
    ) : IJobSchedulerService
{
    public async Task ScheduleJob(IJobRequest? request, ScheduledJobType type)
    {
        var executionResolver = serviceProvider.GetRequiredService<IJobExecutionResolver>();

        var (ctx, job, trigger) = executionResolver.BuildAll(request, type);
        await scheduler.ScheduleJob(job, trigger);
    }

    public async Task ScheduleBackgroundJob()
    {
        var scheduledJobsRepo = serviceProvider.GetRequiredService<IScheduledJobRepository>();
        var resolver = serviceProvider.GetRequiredService<IJobExecutionResolver>();
        var jobs = await scheduledJobsRepo.GetDefaultAndEnabledJobs();

        foreach (var jobEntity in jobs)
        {
            var jobKey = new JobKey(jobEntity.JobKey, jobEntity.JobGroup);
            var existing = await scheduler.GetJobDetail(jobKey);
            if (existing != null) continue;

            var job = resolver.CreateJob(jobEntity);
            var trigger = resolver.CreateTrigger(jobEntity);

            await scheduler.ScheduleJob(job, trigger);
            Log.Information($"✅ Job scheduled: {jobEntity.JobKey}");
        }
    }

    /// <summary>
    /// 清除所有调度中的 Job（包括所有组和触发器）
    /// </summary>
    public async Task ClearAllJobs()
    {
        await scheduler.Clear();
        Log.Information("🧨 All jobs and triggers cleared.");
    }
    // <summary>
    /// 根据指定 Group 名称清除所有 Job（包含其对应的 Trigger）
    /// </summary>
    public async Task ClearJobsByGroup(string groupName)
    {
        var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName));

        foreach (var jobKey in jobKeys)
        {
            await scheduler.DeleteJob(jobKey);
            Log.Information($"🗑 Job {jobKey} in group '{groupName}' deleted.");
        }
    }

    /// <summary>
    /// 根据指定 Group 名称清除所有 Trigger（保留 Job 本体）
    /// </summary>
    public async Task ClearTriggersByGroup(string groupName)
    {
        var triggerKeys = await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(groupName));

        foreach (var triggerKey in triggerKeys)
        {
            await scheduler.UnscheduleJob(triggerKey);
            Log.Information($"⏱ Trigger {triggerKey} in group '{groupName}' unscheduled.");
        }
    }

    /// <summary>
    /// 精确清除指定 JobKey（名称匹配，支持跨组查找）
    /// </summary>
    public async Task ClearSpecificJobByKey(string jobKey)
    {
        var allJobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
        var targetJobKey = allJobKeys.FirstOrDefault(jk => jk.Name.Equals(jobKey, StringComparison.OrdinalIgnoreCase));

        if (targetJobKey == null)
        {
            Log.Information($"❌ Job '{jobKey}' not found.");
            return;
        }

        var result = await scheduler.DeleteJob(targetJobKey);

        Log.Information(result
            ? $"✅ Job '{jobKey}' cleared."
            : $"⚠️ Failed to clear job '{jobKey}'.");
    }

    /// <summary>
    /// 清除所有属于用户自定义任务（默认 Group 名为 'UserJobs'）
    /// </summary>
    public async Task ClearAllUserJobs()
    {
        var userJobs = await scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals("UserJobs"));

        foreach (var jobKey in userJobs)
        {
            await scheduler.DeleteJob(jobKey);
            Log.Information($"🧹 Cleared user job: {jobKey.Name}");
        }
    }

    /// <summary>
    /// 清除已过期的一次性任务（Trigger.RepeatCount=0 且已结束）
    /// </summary>
    public async Task ClearExpiredOneTimeJobs()
    {
        var allTriggers = await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());

        foreach (var triggerKey in allTriggers)
        {
            var trigger = await scheduler.GetTrigger(triggerKey);

            if (trigger is ISimpleTrigger simpleTrigger &&
                simpleTrigger.RepeatCount == 0 &&
                simpleTrigger.EndTimeUtc.HasValue &&
                simpleTrigger.EndTimeUtc.Value < DateTimeOffset.UtcNow)
            {
                await scheduler.UnscheduleJob(triggerKey);
                Log.Information($"🗑 Removed expired one-time trigger: {triggerKey.Name}");
            }
        }
    }
}
