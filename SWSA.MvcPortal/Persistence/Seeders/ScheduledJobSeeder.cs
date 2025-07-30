using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Commons.Quartz.Support;
using SWSA.MvcPortal.Entities.Systems;

namespace SWSA.MvcPortal.Persistence.Seeders;


public class ScheduledJobSeeder(AppDbContext db) : ISeeder
{
    public async Task Seed()
    {
        if (!await db.Database.CanConnectAsync())
        {
            return;
        }

        var jobs = GetDefaultJobs();

        var jobKeys = jobs.Select(c => c.JobKey).ToList();
        var existingJobKeys = await db.ScheduledJobs
          .Where(c => !c.IsCustom && jobKeys.Contains(c.JobKey))
          .Select(c => c.JobKey)
          .ToListAsync();
        var toAdd = jobs.Where(j => !existingJobKeys.Contains(j.JobKey)).ToList();

        if (toAdd.Count != 0)
        {
            using var transaction = db.Database.BeginTransaction();
            try
            {

                await db.AddRangeAsync(toAdd);
                await db.SaveChangesAsync();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    private List<ScheduledJob> GetDefaultJobs()
    {
        return [
        new ()
        {
            JobKey = QuartzJobKeys.AssignmentDueSoonJobKey.Name,
            JobGroup = QuartzJobKeys.AssignmentDueSoonJobKey.Group,
            JobType= ScheduledJobType.AssignmentDueSoon,
            ScheduleType = ScheduleType.Daily,
            CronExpression = CronExpressionBuilder.DailyAt("9:00"),
            IsEnabled = true,
            IsCustom = false,
            CreatedAt = DateTime.Now
        },
          new ()
        {
            JobKey = QuartzJobKeys.AssignmentRemindJobKey.Name,
            JobGroup = QuartzJobKeys.AssignmentRemindJobKey.Group,
            JobType= ScheduledJobType.AssignmentRemind,
            ScheduleType = ScheduleType.Daily,
            CronExpression = CronExpressionBuilder.DailyAt("9:00"),
            IsEnabled = true,
            IsCustom = false,
            CreatedAt = DateTime.Now
        },
       ];
    }
}

