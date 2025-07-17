using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Persistence.QueryExtensions;

public static class WorkAssignmentUserMappingQueryExtensions
{
    public static IQueryable<WorkAssignmentUserMapping> GetTodayRemind(this IQueryable<WorkAssignmentUserMapping> query)
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        return query.Where(
            c => c.WorkAssignment.ReminderDate >= today 
        && c.WorkAssignment.ReminderDate < tomorrow);
    }

    public static async Task<WorkAssignmentUserMapping?> GetByTaskIdAndUserId(this IQueryable<WorkAssignmentUserMapping> query, int taskId, int userId)
    {
        return await query.FirstOrDefaultAsync(c => c.WorkAssignmentId == taskId && c.UserId == userId);
    }

    public static async Task<bool> ExistAsyn(this IQueryable<WorkAssignmentUserMapping> query, int taskId, int userId)
    {
        return await query.AnyAsync(c => c.WorkAssignmentId == taskId && c.UserId == userId);
    }
}
