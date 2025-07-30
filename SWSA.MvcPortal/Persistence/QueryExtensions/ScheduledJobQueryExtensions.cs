using SWSA.MvcPortal.Entities.Systems;

namespace SWSA.MvcPortal.Persistence.QueryExtensions;

public static class ScheduledJobQueryExtensions
{
    public static IQueryable<ScheduledJob> GetDefaultJobs(this IQueryable<ScheduledJob> query)
    {
        return query.Where(c => !c.IsCustom);
    }
}
