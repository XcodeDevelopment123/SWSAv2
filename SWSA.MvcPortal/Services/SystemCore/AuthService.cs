using AutoMapper;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Commons.Services.Session;
using SWSA.MvcPortal.Dtos.Responses;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Services.Interfaces.SystemCore;

namespace SWSA.MvcPortal.Services.SystemCore;

public class AuthService(
    IUserRepository userRepo,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IUserSessionWriter userSessionWriter
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

        userSessionWriter.Write(user);

        user.LastLoginAt = DateTime.Now;
        userRepo.Update(user);
        await userRepo.SaveChangesAsync();
        return new LoginResult().Success(user.StaffId);
    }

    public void Logout()
    {
        _session.Clear();
    }
}
