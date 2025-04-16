using Quartz;
using SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;

namespace SWSA.MvcPortal.Commons.Quartz.Jobs;

public class AssignmentDueSoonJob(
    ILogger<AssignmentDueSoonJob> logger,
    IAssignmentDueSoonJobService service
    ) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("[AssignmentDueSoonJob] Execute time: {Time}", DateTime.Now);
        try
        {
            await service.ProcessDueSoonAssignmentsAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[AssignmentDueSoonJob] Error occurred while processing due soon assignments.");
            //Can add system log service to log error
        }

        await Task.CompletedTask;
    }
}
