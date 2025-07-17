using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Persistence.QueryExtensions;

public static class MsicCodeQueryExtensions
{
    public static IQueryable<CompanyMsicCode> GetMsicCodes(this IQueryable<CompanyMsicCode> query, List<int> ids)
    {
        return query.Where(c => ids.Contains(c.Id));
    }

    public static IQueryable<MsicCode> GetMsicCodes(this IQueryable<MsicCode> query, List<int> ids)
    {
        return query.Where(c => ids.Contains(c.Id));
    }
}
