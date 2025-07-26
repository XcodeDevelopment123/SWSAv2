using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.WorkAllocations;

namespace SWSA.MvcPortal.Persistence.QueryExtensions;

public static class WorkAllocationQueryExtensions
{
    public static async Task<bool> ClientServiceExistAsync(this IQueryable<ClientWorkAllocation> query,int clientId,int? excludeId,ServiceScope service)
    {
        return await query.AnyAsync(c =>
    c.ClientId == clientId &&
    c.ServiceScope == service &&
    (!excludeId.HasValue || c.Id != excludeId.Value));
    }
}
