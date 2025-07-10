
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Repositories.Interfaces;

public interface IUserRepository : IRepositoryBase<User>
{
    // Define your method here
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByStaffIdAsync(string staffId);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<List<User>> GetByActiveStatus(bool isActive);
    Task<Dictionary<string, User>> GetDictionaryByStaffIdsAsync(List<string> staffIds);
    Task<Dictionary<string, int>> GetDictionaryIdByStaffIdsAsync(List<string> staffIds);
    Task<User> GetOverviewByStaffIdAsync(string staffId);
    Task<UserOverviewVM> GetOverviewVMByStaffIdAsync(string staffId);
}
