namespace SWSA.MvcPortal.Services.Interfaces;

public interface IUserContext
{
    string Name { get; }    
    string StaffId { get; }
    int? CompanyId { get; }
    int? CompanyDepartmentId { get; }
    bool IsCompanyStaff { get; }

    DateTime LoginTime { get; }
    int EntityId { get; }
}
