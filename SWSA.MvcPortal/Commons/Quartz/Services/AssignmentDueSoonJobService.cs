using Serilog;
using SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;
using SWSA.MvcPortal.Commons.Services.Messaging.TemplateData;
using SWSA.MvcPortal.Repositories.Interfaces;

namespace SWSA.MvcPortal.Commons.Quartz.Services;


[Obsolete("currently use job instead of service")]
public class AssignmentDueSoonJobService(
    ICompanyWorkAssignmentRepository companyWorkAssignmentRepository
    //Inject messaging service to use whatsapp / email etc..
    ) : IAssignmentDueSoonJobService
{
    
    public async Task ProcessDueSoonAssignmentsAsync()
    {
        var now = DateTime.Today;
        // Retrieve all assignments from database (later should be filtered by repo)
        var tasks = await companyWorkAssignmentRepository.GetDueSoonAssignments();
       if(tasks.Count == 0)
        {
            // No tasks due soon
            return;
        }


        Log.Information($"[{tasks.Count}] due soon assignment found");


        // TODO: 可继续进行：
        // - 过滤 IsCompleted == false
        // - 过滤 DueDate <= now.AddDays(3)
        // - 写入通知表 SystemNotification

    }
}
