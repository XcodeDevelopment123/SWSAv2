using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Services;

public class UserContext : IUserContext
{
    public string Name { get; }
    public string StaffId { get; }
    public int? CompanyId { get; }
    public int? CompanyDepartmentId { get; }
    public DateTime LoginTime { get; }
    public bool IsCompanyStaff => CompanyId.HasValue;

    public UserContext(IHttpContextAccessor accessor)
    {
        var session = accessor.HttpContext?.Session;

        StaffId = session?.GetString(SessionKeys.StaffId) ?? throw new UnauthorizedAccessException("Not logged in");
        Name = session?.GetString(SessionKeys.Name) ?? throw new UnauthorizedAccessException("Not logged in");
        CompanyId = TryParseInt(session?.GetString(SessionKeys.CompanyId));
        CompanyDepartmentId = TryParseInt(session?.GetString(SessionKeys.CompanyDepartmentId));
        LoginTime = DateTime.TryParse(session?.GetString(SessionKeys.LoginTime), out var time)
            ? time
            : DateTime.MinValue;
    }

    private int? TryParseInt(string? input)
    {
        return int.TryParse(input, out var result) ? result : (int?)null;
    }
}
