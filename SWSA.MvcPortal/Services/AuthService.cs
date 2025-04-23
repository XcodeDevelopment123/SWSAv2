using AutoMapper;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Exceptions;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Dtos.Responses;
using SWSA.MvcPortal.Models.CompanyStaffs;
using SWSA.MvcPortal.Models.Users;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Services;

public class AuthService(
    IUserRepository userRepo,
    ICompanyStaffRepository companyStaffRepo,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
    ) : IAuthService
{
    private readonly ISession _session =
       httpContextAccessor.HttpContext?.Session
       ?? throw new InvalidOperationException("Session is not available.");
    public async Task<LoginResult> Login(string username, string password)
    {
        var user = await userRepo.GetByUsernameAsync(username);
        if (user == null)
        {
            return new LoginResult().Failed(LoginResult.LoginResultType.UserNotFound);
        }

        if (!PasswordHasher.Verify(password, user.HashedPassword))
        {
            return new LoginResult().Failed(LoginResult.LoginResultType.InvalidPassword);
        }

        if (PasswordHasher.NeedsRehash(user.HashedPassword))
        {
            user.HashedPassword = PasswordHasher.Hash(password);
        }

        var userVM = mapper.Map<UserVM>(user);

        _session.SetString(SessionKeys.EntityId, user.Id.ToString());
        _session.SetString(SessionKeys.StaffId, user.StaffId);
        _session.SetString(SessionKeys.Name, user.FullName);
        _session.SetString(SessionKeys.LoginTime, DateTime.Now.ToString());

        user.LastLoginAt = DateTime.Now;
        userRepo.Update(user);
        await userRepo.SaveChangesAsync();
        return new LoginResult().Success(user.StaffId);
    }

    public async Task<LoginResult> PartnerLogin(string username, string password)
    {
        var staff = await companyStaffRepo.GetByUsernameAsync(username);
        if (staff == null)
            return new LoginResult().Failed(LoginResult.LoginResultType.UserNotFound);

        if (!staff.IsLoginEnabled || string.IsNullOrEmpty(staff.HashedPassword))
            return new LoginResult().Failed(LoginResult.LoginResultType.AccountNotEnable);


        if (!PasswordHasher.Verify(password, staff.HashedPassword))
        {
            return new LoginResult().Failed(LoginResult.LoginResultType.InvalidPassword);
        }

        if (PasswordHasher.NeedsRehash(staff.HashedPassword))
        {
            staff.HashedPassword = PasswordHasher.Hash(password);
        }

        var staffVM = mapper.Map<CompanyStaffVM>(staff);

        _session.SetString(SessionKeys.EntityId, staff.Id.ToString());
        _session.SetString(SessionKeys.StaffId, staff.StaffId);
        _session.SetString(SessionKeys.CompanyId, staff.CompanyId.ToString());
        if (staff.CompanyDepartmentId.HasValue)
        {
            _session.SetString(SessionKeys.CompanyDepartmentId, staff.CompanyDepartmentId.ToString() ?? "");
        }
        _session.SetString(SessionKeys.Name, staffVM.ContactName);
        _session.SetString(SessionKeys.LoginTime, DateTime.Now.ToString());

        staff.LastLoginAt = DateTime.Now;
        companyStaffRepo.Update(staff);
        await companyStaffRepo.SaveChangesAsync();
        return new LoginResult().Success(staff.StaffId);
    }

    public void Logout()
    {
        _session.Clear();
    }

}
