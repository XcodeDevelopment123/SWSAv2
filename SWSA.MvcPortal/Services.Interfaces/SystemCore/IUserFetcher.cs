using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Services.Interfaces.SystemCore;

public interface IUserFetcher
{
    Task<User?> GetByStaffId(string staffId);
}
