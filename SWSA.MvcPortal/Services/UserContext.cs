using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Services;

public class UserContext : IUserContext
{
    public string Name { get; }
    public int EntityId { get; }
    public string StaffId { get; }

    public DateTime LoginTime { get; }

    public UserContext(IHttpContextAccessor accessor)
    {
        var session = accessor.HttpContext?.Session;

        EntityId = TryParseInt(session?.GetString(SessionKeys.EntityId)) ?? 0;
        StaffId = session?.GetString(SessionKeys.StaffId) ?? "";
        Name = session?.GetString(SessionKeys.Name) ?? "";
        LoginTime = DateTime.TryParse(session?.GetString(SessionKeys.LoginTime), out var time)
            ? time
            : DateTime.MinValue;
    }

    private int? TryParseInt(string? input)
    {
        return int.TryParse(input, out var result) ? result : (int?)null;
    }
}
