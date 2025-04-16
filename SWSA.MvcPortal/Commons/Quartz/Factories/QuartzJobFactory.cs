using Quartz.Spi;
using Quartz;

namespace SWSA.MvcPortal.Commons.Quartz.Factories;

public class QuartzJobFactory : IJobFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public QuartzJobFactory(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        // ✅ 使用作用域解析 Scoped Job
        var scope = _serviceScopeFactory.CreateScope();

        try
        {
            var job = (IJob)scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType);
            return job;
        }
        catch
        {
            scope.Dispose(); // 避免泄露
            throw;
        }
    }

    public void ReturnJob(IJob job)
    {
        // 默认不处理，作用域会自动释放
    }
}
