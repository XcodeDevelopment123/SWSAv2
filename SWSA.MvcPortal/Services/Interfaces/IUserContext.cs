using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface IUserContext
{
    string Name { get; }    
    string StaffId { get; }
    DateTime LoginTime { get; }
    int EntityId { get; }
    UserRole Role { get; }
    bool IsSuperAdmin { get; }
    List<int> AllowedCompanyIds { get; }
    Dictionary<int, List<string>> AllowedDepartments { get; } // CompanyId → List<string> == Department name
}
