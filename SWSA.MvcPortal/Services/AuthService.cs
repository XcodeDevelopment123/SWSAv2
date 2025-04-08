using AutoMapper;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Dtos.Responses;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Services;

public class AuthService(
    IUserRepository userRepo,
    IMapper mapper
    ) : IAuthService
{

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

        user.LastLoginAt = DateTime.Now;
        userRepo.Update(user);
        await userRepo.SaveChangesAsync();
        return new LoginResult().Success(user.StaffId);
    }
}
