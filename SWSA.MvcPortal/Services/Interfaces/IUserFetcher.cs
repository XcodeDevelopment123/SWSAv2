using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface IUserFetcher
{
    Task<User?> GetByStaffId(string staffId);
}
