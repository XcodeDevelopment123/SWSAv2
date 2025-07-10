using SWSA.MvcPortal.Dtos.Requests.Users;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Services.Interfaces.UserAccess;

public interface IUserService
{
    Task<string> Create(CreateUserRequest req);
    Task<UserVM> DeleteUserByIdAsync(string staffId);
    Task<UserVM> GetUserByIdAsync(string staffId);
    Task<UserOverviewVM> GetUserOverviewVMAsync(string staffId);
    Task<List<UserListVM>> GetUsersAsync();
    Task<List<UserSelectionVM>> GetUserSelectionAsync();
    Task<bool> Edit(EditUserRequest req);
}
