
using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Persistence.QueryExtensions;

namespace SWSA.MvcPortal.Commons.Quartz.Jobs;

public class AssignmentDueSoonJob(
    AppDbContext db
    ) : IJob
{

    private readonly DbSet<WorkAssignment> workAssignments = db.Set<WorkAssignment>();
   
    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information("[AssignmentDueSoonJob] Execute time: {Time}", DateTime.Now);
        try
        {
            var tasks = await workAssignments.GetDuesoon().ToListAsync();
            if (tasks.Count > 0)
            {

                ///TODO : Uncomment and implement the logic to send WhatsApp messages
                //var groupedTasks = tasks
                // .Where(t => t.AssignedUser != null && !string.IsNullOrEmpty(t.AssignedUser.PhoneNumber))
                // .GroupBy(t => t.AssignedUser?.Id);

                //foreach (var group in groupedTasks)
                //{
                //    var user = group.First().AssignedUser!;
                //    var whatsapp = user.GetWhatsappNumber();

                //    var taskListText = string.Join("\n\n", group.Select(ts =>
                //    {
                //        var daysLeft = (ts.DueDate.Date - DateTime.Today).Days;
                //        return $"""
                //            *Task:* {ts.ServiceScope}
                //            *Task ID:* {ts.Id}
                //            *Company:* {ts.Company.Name} ({ts.Company.RegistrationNumber})
                //            *Due Date:* {ts.DueDate:yyyy-MM-dd} ({daysLeft} day{(daysLeft == 1 ? "" : "s")} left)
                //            """;
                //    }));

                //    var wappyMessage = new WappyTemplateData
                //    {
                //        Body = $"""
                //            [SWSA] Reminder 🕒

                //            You have {group.Count()} upcoming task{(group.Count() > 1 ? "s" : "")} nearing deadline:

                //            {taskListText}

                //            Please review and take necessary action as soon as possible.
                //            *This is system auto generated*
                //            """
                //    };
                //    await messagingService.SendAsync(
                //        MessagingChannel.Wappy,
                //        whatsapp,
                //        MessagingTemplateCode.AssignmentWorkDueSoon,
                //        TemplateDataBuilder.From(wappyMessage),
                //        "Reminder: tasks due soon"
                //    );
                //}
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
