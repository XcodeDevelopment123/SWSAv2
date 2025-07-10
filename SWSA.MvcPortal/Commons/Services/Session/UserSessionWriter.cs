using Newtonsoft.Json;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Commons.Services.Session;

public class UserSessionWriter(IHttpContextAccessor _accessor):IUserSessionWriter
{
    public void Write(User user)
    {
        var session = _accessor.HttpContext?.Session!;
        session.SetString(SessionKeys.EntityId, user.Id.ToString());
        session.SetString(SessionKeys.StaffId, user.StaffId);
        session.SetString(SessionKeys.Name, user.FullName);
        session.SetString(SessionKeys.LoginTime, DateTime.Now.ToString());
        session.SetString(SessionKeys.UserRole, user.Role.ToString());
    }
}
