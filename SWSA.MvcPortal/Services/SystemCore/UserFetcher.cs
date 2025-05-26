using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Services.Interfaces.SystemCore;

namespace SWSA.MvcPortal.Services.SystemCore;

public class UserFetcher(IUserRepository _repo) : IUserFetcher
{
    public async Task<User?> GetByStaffId(string staffId)
    {
        return await _repo.GetByStaffIdAsync(staffId);
    }
}
