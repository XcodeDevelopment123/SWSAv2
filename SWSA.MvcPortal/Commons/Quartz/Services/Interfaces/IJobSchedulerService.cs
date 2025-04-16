using SWSA.MvcPortal.Commons.Quartz.Requests;
using SWSA.MvcPortal.Commons.Quartz.Support;

namespace SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;

public interface IJobSchedulerService
{
    Task ClearAllJobs();
    Task ScheduleBackgroundJob();
    Task ScheduleJob(IJobRequest? request, QuratzJobType type);
}
