using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Services.Interfaces.Auths;

namespace SWSA.MvcPortal.Services.Auths;

public class UserContext : IUserContext
{
    public string Name { get; }
    public int EntityId { get; }
    public string StaffId { get; }
    public DateTime LoginTime { get; }
    public UserRole Role { get; }

    public UserContext(IHttpContextAccessor accessor)
    {
        var session = accessor.HttpContext?.Session;

        var hasEntityId = int.TryParse(session?.GetString(SessionKeys.EntityId), out var entityId);
        var hasRole = Enum.TryParse<UserRole>(session?.GetString(SessionKeys.UserRole), out var parsedRole);
        var hasLoginTime = DateTime.TryParse(session?.GetString(SessionKeys.LoginTime), out var time);

        EntityId = hasEntityId ? entityId : 0;
        StaffId = session?.GetString(SessionKeys.StaffId) ?? "";
        Name = session?.GetString(SessionKeys.Name) ?? "";
        Role = hasRole ? parsedRole : UserRole.Unknown;
        LoginTime = hasLoginTime ? time : DateTime.MinValue;
    }

    public bool IsSuperAdmin => Role == UserRole.SuperAdmin;
}
