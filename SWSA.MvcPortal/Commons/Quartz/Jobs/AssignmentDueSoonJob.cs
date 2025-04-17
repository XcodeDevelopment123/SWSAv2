
using Quartz;
using Serilog;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Services.Messaging.Enums;
using SWSA.MvcPortal.Commons.Services.Messaging.TemplateData;
using SWSA.MvcPortal.Repositories.Interfaces;

namespace SWSA.MvcPortal.Commons.Quartz.Jobs;

public class AssignmentDueSoonJob(
    ICompanyWorkAssignmentRepository companyWorkAssignmentRepository,
    IMessagingService messagingService
    ) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information("[AssignmentDueSoonJob] Execute time: {Time}", DateTime.Now);
        try
        {
            var tasks = await companyWorkAssignmentRepository.GetDueSoonAssignments();
            if (tasks.Count == 0)
            {
                // No tasks due soon
                return;
            }

            foreach (var ts in tasks)
            {
                if (ts.AssignedStaff == null)
                {
                    Log.Warning($"[AssignmentDueSoonJob] Task \"{ts.ServiceScope}\" (ID: {ts.Id}) has no assigned staff");
                    continue;
                }

                if (string.IsNullOrEmpty(ts.AssignedStaff.WhatsApp))
                {
                    Log.Information($"[AssignmentDueSoonJob] {ts.AssignedStaff.ContactName} has no whatsapp");
                    continue;
                }

                var whatsapp = ts.AssignedStaff.GetWhatsappNumber();
                var daysLeft = (ts.DueDate.Date - DateTime.Today).Days;
                var wappyMessage = new WappyTemplateData()
                {
                    WhatsappName = "zTemp8620",
                    Body = $"""
                    [SWSA] Reminder 🕒

                    *Task:* {ts.ServiceScope}
                    *Task ID:* {ts.Id}
                    *Company:* {ts.Company.Name} ({ts.Company.RegistrationNumber})
                    *Due Date:* {ts.DueDate:yyyy-MM-dd}  ({daysLeft} day{(daysLeft == 1 ? "" : "s")} left)
                    
                    Please review and take necessary action as soon as possible.
                    """
                };
                await messagingService.SendAsync(MessagingChannel.Wappy,
                         whatsapp, MessagingTemplateCode.AssignmentWorkDueSoon, TemplateDataBuilder.From(wappyMessage),
                        "due date nearing");
            }



            Log.Information($"[AssignmentDueSoonJob] {tasks.Count} due soon assignment found");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "[AssignmentDueSoonJob] Error occurred while processing due soon assignments.");
            //Can add system log service to log error
        }

        await Task.CompletedTask;
    }
}
