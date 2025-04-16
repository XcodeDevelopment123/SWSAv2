using Quartz;
using SWSA.MvcPortal.Commons.Quartz.Requests;

namespace SWSA.MvcPortal.Commons.Quartz.Factories;

public interface IJobBaseFactory
{
    IJobDetail CreateJob(IJobRequest? request);
    ITrigger CreateTrigger(IJobRequest? request);
}
