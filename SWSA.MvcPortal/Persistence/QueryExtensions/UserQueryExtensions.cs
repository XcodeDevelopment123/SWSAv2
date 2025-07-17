using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Persistence.QueryExtensions;

public static class UserQueryExtensions
{
    public static IQueryable<User> GetActive(this IQueryable<User> query)
    {
        return query.Where(c => c.IsActive);
    }

    public static async Task<bool> ExistUsernameAsync(this IQueryable<User> query, string username)
    {
        return await query.AnyAsync(c => c.Username == username);
    }

    public static async Task<bool> ExistAsync(this IQueryable<User> query, string staffId)
    {
        return await query.AnyAsync(c => c.StaffId == staffId);
    }

    public static async Task<Dictionary<string, int>> GetUserIdDictionaryByStaffIdAsync(this IQueryable<User> query, List<string> staffIds)
    {
        return await query.Where(c => staffIds.Contains(c.StaffId)).ToDictionaryAsync(c => c.StaffId, c => c.Id);
    }


}
