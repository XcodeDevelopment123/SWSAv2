using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Commons.Services.Session;
using SWSA.MvcPortal.Dtos.Responses;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Services.Interfaces.Auths;

namespace SWSA.MvcPortal.Services.Auths;

public class AuthService(
    IHttpContextAccessor httpContextAccessor,
    IUserSessionWriter userSessionWriter,
    AppDbContext db
    ) : IAuthService
{
    private readonly DbSet<User> users = db.Set<User>();

    private readonly ISession _session =
       httpContextAccessor.HttpContext?.Session
       ?? throw new InvalidOperationException("Session is not available.");
    public async Task<LoginResult> Login(string username, string password)
    {
        var user = await users.FirstOrDefaultAsync(c => c.Username == username);
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
        db.Update(user);
        await db.SaveChangesAsync();
        return new LoginResult().Success(user.StaffId);
    }

    public void Logout()
    {
        _session.Clear();
    }
}
