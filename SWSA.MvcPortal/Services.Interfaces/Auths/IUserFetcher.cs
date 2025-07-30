using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Services.Interfaces.Auths;

public interface IUserFetcher
{
    Task<User?> GetByStaffId(string staffId);
}
