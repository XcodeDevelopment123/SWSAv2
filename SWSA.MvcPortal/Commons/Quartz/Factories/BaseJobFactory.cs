using Quartz;
using SWSA.MvcPortal.Commons.Quartz.Requests;
namespace SWSA.MvcPortal.Commons.Quartz.Factories;

public class BaseJobFactory : IJobBaseFactory
{
    public virtual IJobDetail CreateJob(IJobRequest? request)
    {
        return null!;
    }

    public virtual ITrigger CreateTrigger(IJobRequest? request)
    {
        return null!;
    }
}
