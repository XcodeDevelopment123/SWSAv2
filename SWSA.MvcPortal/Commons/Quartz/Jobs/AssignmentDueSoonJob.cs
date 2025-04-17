
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

            var groupedTasks = tasks
                .Where(t => t.AssignedStaff != null && !string.IsNullOrEmpty(t.AssignedStaff.WhatsApp))
                .GroupBy(t => t.AssignedStaff?.Id);

            foreach (var group in groupedTasks)
            {
                var staff = group.First().AssignedStaff!;
                var whatsapp = staff.GetWhatsappNumber();

                var taskListText = string.Join("\n\n", group.Select(ts =>
                {
                    var daysLeft = (ts.DueDate.Date - DateTime.Today).Days;
                    return $"""
                            *Task:* {ts.ServiceScope}
                            *Task ID:* {ts.Id}
                            *Company:* {ts.Company.Name} ({ts.Company.RegistrationNumber})
                            *Due Date:* {ts.DueDate:yyyy-MM-dd} ({daysLeft} day{(daysLeft == 1 ? "" : "s")} left)
                            """;
                }));

                var wappyMessage = new WappyTemplateData
                {
                    WhatsappName = staff.ContactName,
                    Body = $"""
                            [SWSA] Reminder 🕒

                            You have {group.Count()} upcoming task{(group.Count() > 1 ? "s" : "")} nearing deadline:

                            {taskListText}

                            Please review and take necessary action as soon as possible.
                            *This is system auto generated*
                            """
                };

                await messagingService.SendAsync(
                    MessagingChannel.Wappy,
                    whatsapp,
                    MessagingTemplateCode.AssignmentWorkDueSoon,
                    TemplateDataBuilder.From(wappyMessage),
                    "Reminder: tasks due soon"
                );
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
