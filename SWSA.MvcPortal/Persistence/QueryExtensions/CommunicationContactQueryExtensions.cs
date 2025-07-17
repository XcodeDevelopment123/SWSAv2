using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Persistence.QueryExtensions;

public static class CommunicationContactQueryExtensions
{
    public static Task<bool> ExistAsync(this IQueryable<CommunicationContact> query, int id)
    {
        return query.AnyAsync(c => c.Id == id);
    }
}
