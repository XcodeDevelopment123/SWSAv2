using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Services.Interfaces.Auths;

public interface IUserContext
{
    string Name { get; }    
    string StaffId { get; }
    DateTime LoginTime { get; }
    int EntityId { get; }
    UserRole Role { get; }
    bool IsSuperAdmin { get; }
}
