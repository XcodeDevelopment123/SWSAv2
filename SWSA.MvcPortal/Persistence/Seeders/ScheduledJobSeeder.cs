using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Commons.Quartz.Support;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Persistence.Seeders;


public class ScheduledJobSeeder(AppDbContext db) : ISeeder
{
    public async Task Seed()
    {
        if (!await db.Database.CanConnectAsync())
        {
            return;
        }

        if (await db.ScheduledJobs.AnyAsync())
            return;

        using var transaction = db.Database.BeginTransaction();
        try
        {
            var jobs = GetDefaultJobs();

            await db.AddRangeAsync(jobs);
            await db.SaveChangesAsync();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
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
       ];
    }
}

