using Serilog;

namespace SWSA.MvcPortal.Commons.Services.BackgroundQueue;

public class AuditLogWorker(IBackgroundTaskQueue taskQueue) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem = await taskQueue.DequeueAsync(stoppingToken);
            try
            {
                await workItem(stoppingToken);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Audit log] worker failed.");
            }
        }
    }
}
