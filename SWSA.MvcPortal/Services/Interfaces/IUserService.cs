
using SWSA.MvcPortal.Dtos.Requests.Users;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface IUserService
{
    Task<string> CreateUser(CreateUserRequest req);
    Task<UserVM> DeleteUserByIdAsync(string staffId);
    Task<UserVM> GetUserByIdAsync(string staffId);
    Task<List<UserListVM>> GetUsersAsync();
    Task<List<UserSelectionVM>> GetUserSelectionAsync();
    Task<bool> SetUserSession(string staffId);
    Task<bool> UpdateUserInfo(EditUserRequest req);
}
