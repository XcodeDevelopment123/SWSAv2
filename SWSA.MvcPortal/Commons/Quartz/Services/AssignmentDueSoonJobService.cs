using SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Commons.Quartz.Services;

public class AssignmentDueSoonJobService(
    ICompanyWorkAssignmentRepository companyWorkAssignmentRepository
    //Inject messaging service to use whatsapp / email etc..
    ) : IAssignmentDueSoonJobService
{
    public async Task ProcessDueSoonAssignmentsAsync()
    {
        var now = DateTime.Today;
        // Retrieve all assignments from database (later should be filtered by repo)
        var tasks = await companyWorkAssignmentRepository.GetAllAsync();
        Console.WriteLine("Retreive");


        // TODO: 可继续进行：
        // - 过滤 IsCompleted == false
        // - 过滤 DueDate <= now.AddDays(3)
        // - 写入通知表 SystemNotification

    }
}
