using AutoMapper;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Exceptions;
using SWSA.MvcPortal.Commons.Guards;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Dtos.Requests.Users;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Services;

public class UserService(
IUserRepository repo,
IMapper mapper,
IHttpContextAccessor httpContextAccessor
) : IUserService
{
    private readonly ISession _session =
     httpContextAccessor.HttpContext?.Session
     ?? throw new InvalidOperationException("Session is not available.");


    public async Task<List<UserListVM>> GetUsersAsync()
    {
        var data = await repo.GetAllAsync();

        return mapper.Map<List<UserListVM>>(data);
    }

    public async Task<List<UserSelectionVM>> GetUserSelectionAsync()
    {
        var data = await repo.GetByActiveStatus(true);
        return mapper.Map<List<UserSelectionVM>>(data);
    }

    public async Task<UserVM> GetUserByIdAsync(string staffId)
    {
        var data = await repo.GetByStaffIdAsync(staffId);
        Guard.AgainstNullData(data, "User not found");

        return mapper.Map<UserVM>(data);
    }

    public async Task<string> CreateUser(CreateUserRequest req)
    {
        if (await repo.ExistsByUsernameAsync(req.Username))
        {
            throw new BusinessLogicException("Username already exists");
        }

        var hashPassword = PasswordHasher.Hash(req.Password);
        var user = mapper.Map<User>(req);
        user.HashedPassword = hashPassword;

        repo.Add(user);
        await repo.SaveChangesAsync();

        return user.StaffId;
    }

    public async Task<bool> UpdateUserInfo(EditUserRequest req)
    {
        var user = await repo.GetByStaffIdAsync(req.StaffId);
        Guard.AgainstNullData(user, "User not found");

        user.FullName = req.FullName;
        user.Email = req.Email;
        user.PhoneNumber = req.PhoneNumber;
        user.IsActive = req.IsActive;
        if (!string.IsNullOrEmpty(req.Password))
        {
            var hashPassword = PasswordHasher.Hash(req.Password);
            user.HashedPassword = hashPassword;
        }

        repo.Update(user);
        await repo.SaveChangesAsync();

        return true;
    }

    public async Task<UserVM> DeleteUserByIdAsync(string staffId)
    {
        var data = await repo.GetByStaffIdAsync(staffId);
        Guard.AgainstNullData(data, "User not found");

        repo.Remove(data!);
        await repo.SaveChangesAsync();

        return mapper.Map<UserVM>(data);
    }

    public async Task<bool> SetUserSession(string staffId)
    {
        var user = await repo.GetByStaffIdAsync(staffId);
        if (user == null)
        {
            return false;
        }

        var userVM = mapper.Map<UserVM>(user);

        _session.SetString(SessionKeys.StaffId, user.StaffId);
        _session.SetString(SessionKeys.Name, user.FullName);
        _session.SetString(SessionKeys.LoginTime, DateTime.Now.ToString());
        return true;
    }
}
