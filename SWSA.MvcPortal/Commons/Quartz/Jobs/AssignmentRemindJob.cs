using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Extensions;
using SWSA.MvcPortal.Commons.Services.Messaging.Enums;
using SWSA.MvcPortal.Commons.Services.Messaging.TemplateData;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Persistence.QueryExtensions;

namespace SWSA.MvcPortal.Commons.Quartz.Jobs;

public class AssignmentRemindJob(
IMessagingService messagingService,
AppDbContext db
) : IJob
{
    private readonly DbSet<WorkAssignmentUserMapping> workUserMapping = db.Set<WorkAssignmentUserMapping>();
    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information("[AssignmentRemindJob] Execute time: {Time}", DateTime.Now);
        try
        {
            var userAssignments = await workUserMapping.GetTodayRemind().ToListAsync();

            var grouped = userAssignments
                .Where(x => x.User != null && !string.IsNullOrEmpty(x.User.PhoneNumber))
                .GroupBy(x => x.User.Id);

            var totalTasks = userAssignments.GroupBy(c => c.WorkAssignmentId).Count();

            if (grouped.Count() > 0)
            {
                foreach (var group in grouped)
                {
                    var user = group.First().User!;
                    var whatsapp = user.GetWhatsappNumber();

                    var taskListText = string.Join("\n\n", group.Select(item =>
                    {
                        var ts = item.WorkAssignment;
                        return $"""
                        *Task:* {ts.WorkType.GetDisplayName()} - {ts.ServiceScope.GetDisplayName()}
                        *Task ID:* {ts.Id}

                        *Progress* {ts.Progress?.Status.GetDisplayName() ?? "-"}
                        *Note:* {ts.InternalNote}
                        """;
                        ///TODO : If task have a due date
                        //var daysLeft = (ts.DueDate.Date - DateTime.Today).Days;
                        //return $"""
                        //    *Task:* {ts.ServiceScope}
                        //    *Task ID:* {ts.Id}
                        //    *Company:* {ts.Company.Name} ({ts.Company.RegistrationNumber})
                        //    *Due Date:* {ts.DueDate:yyyy-MM-dd} ({daysLeft} day{(daysLeft == 1 ? "" : "s")} left)
                        //    """;
                    }));

                    var wappyMessage = new WappyTemplateData
                    {
                        Body = $"""
                        [SWSA] Reminder 🕒

                        You have {group.Count()} task{(group.Count() > 1 ? "s" : "")} scheduled for reminder today:

                        {taskListText}

                        Please review and take necessary action if needed.
                        *This is a system-generated message*
                        """
                    };
                    await messagingService.SendAsync(
                        MessagingChannel.Wappy,
                        whatsapp,
                        MessagingTemplateCode.AssignmentRemind,
                        TemplateDataBuilder.From(wappyMessage),
                        $"Reminder: Scheduled Remind at {DateTime.Today.ToFormattedString()}"
                    );
                }
            }

            Log.Information($"[AssignmentRemindJob] {totalTasks} need to remind today found found");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "[AssignmentRemindJob] Error occurred while processing remind assignments.");
            //Can add system log service to log error
        }
        await Task.CompletedTask;
    }
}
