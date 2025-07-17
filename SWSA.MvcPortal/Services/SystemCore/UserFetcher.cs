using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Services.Interfaces.SystemCore;

namespace SWSA.MvcPortal.Services.SystemCore;

public class UserFetcher(
    AppDbContext db
    ) : IUserFetcher
{
    private readonly DbSet<User> users = db.Set<User>();

    public async Task<User?> GetByStaffId(string staffId)
    {
        return await users.FirstOrDefaultAsync(c => c.StaffId == staffId);
    }
}
