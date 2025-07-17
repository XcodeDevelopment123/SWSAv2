using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Persistence.QueryExtensions;

public static class WorkAssignmentQueryExtensions
{
    public static IQueryable<WorkAssignment> GetDuesoon(this IQueryable<WorkAssignment> query, int day = 7)
    {
        var today = DateTime.Today;
        var endDate = today.AddDays(day);

        //Should be due date
        return query.Where(c => c.ReminderDate >= today && c.ReminderDate <= endDate);
    }
}
