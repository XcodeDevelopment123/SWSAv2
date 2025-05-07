using Newtonsoft.Json;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Services;

public class UserContext : IUserContext
{
    public string Name { get; }
    public int EntityId { get; }
    public string StaffId { get; }
    public DateTime LoginTime { get; }
    public UserRole Role { get; }

    public List<int> AllowedCompanyIds { get; }
    public Dictionary<int, List<int>> AllowedDepartments { get; }

    public UserContext(IHttpContextAccessor accessor)
    {
        var session = accessor.HttpContext?.Session;

        EntityId = TryParseInt(session?.GetString(SessionKeys.EntityId)) ?? 0;
        StaffId = session?.GetString(SessionKeys.StaffId) ?? "";
        Name = session?.GetString(SessionKeys.Name) ?? "";
        Role = Enum.TryParse<UserRole>(session?.GetString(SessionKeys.UserRole), out var parsedRole)
            ? parsedRole
            : UserRole.Unknown;
        LoginTime = DateTime.TryParse(session?.GetString(SessionKeys.LoginTime), out var time)
            ? time
            : DateTime.MinValue;
        AllowedCompanyIds = DeserializeListInt(session?.GetString(SessionKeys.AllowedCompanyIds));
        AllowedDepartments = DeserializeDictListInt(session?.GetString(SessionKeys.AllowedDepartments));
    }

    public bool IsSuperAdmin => Role == UserRole.SuperAdmin;

    private int? TryParseInt(string? input)
    {
        return int.TryParse(input, out var result) ? result : (int?)null;
    }

    private List<int> DeserializeListInt(string? json)
    {
        return string.IsNullOrWhiteSpace(json)
            ? new List<int>()
            : JsonConvert.DeserializeObject<List<int>>(json) ?? [];
    }

    private Dictionary<int, List<int>> DeserializeDictListInt(string? json)
    {
        return string.IsNullOrWhiteSpace(json)
            ? []
            : JsonConvert.DeserializeObject<Dictionary<int, List<int>>>(json)
                ?? [];
    }
}
