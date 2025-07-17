using Serilog;
using SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;

namespace SWSA.MvcPortal.Commons.Quartz.Services;


[Obsolete("currently use job instead of service")]
public class AssignmentDueSoonJobService(
    //Inject messaging service to use whatsapp / email etc..
    ) : IAssignmentDueSoonJobService
{
    
    public async Task ProcessDueSoonAssignmentsAsync()
    {
     
        Log.Information($"[0] due soon assignment found");


        // TODO: 可继续进行：
        // - 过滤 IsCompleted == false
        // - 过滤 DueDate <= now.AddDays(3)
        // - 写入通知表 SystemNotification

    }
}
