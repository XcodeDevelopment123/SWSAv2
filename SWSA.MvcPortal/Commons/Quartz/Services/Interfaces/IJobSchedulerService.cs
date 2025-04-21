using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Quartz.Requests;

namespace SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;

public interface IJobSchedulerService
{
    Task ClearAllJobs();
    Task ClearJobsByGroup(string groupName);
    Task ClearSpecificJobByKey(string jobKey);
    Task ClearTriggersByGroup(string groupName);
    Task ScheduleBackgroundJob();
    Task ScheduleJob(IJobRequest? request, ScheduledJobType type);
}
