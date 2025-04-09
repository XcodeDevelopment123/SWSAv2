using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Models.Users;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Services;

public class UserService(
IUserRepository userRepo,
IMapper mapper,
IHttpContextAccessor httpContextAccessor
) : IUserService
{
    private readonly ISession _session =
     httpContextAccessor.HttpContext?.Session
     ?? throw new InvalidOperationException("Session is not available.");
    public async Task<bool> SetUserSession(string staffId)
    {
        var user = await userRepo.GetByStaffIdAsync(staffId);
        if (user == null)
        {
            return false;
        }

        var userVM = mapper.Map<UserVM>(user);

        _session.SetString(SessionKeys.StaffId, user.StaffId);
        _session.SetString(SessionKeys.UserJson, userVM.ToJsonData());
        _session.SetString(SessionKeys.LoginTime, DateTime.Now.ToString());
        return true;
    }
}
