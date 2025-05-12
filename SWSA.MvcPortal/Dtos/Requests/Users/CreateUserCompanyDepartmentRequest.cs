using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Dtos.Requests.Users;

public class CreateUserCompanyDepartmentRequest
{
    public string StaffId { get; set; }
    public List<int> DepartmentIds { get; set; } = new();
    public int? CompanyId { get; set; }
}

public static class UserCompanyDepartmentMapper
{
    public static List<UserCompanyDepartment> ToEntities(List<int> dptIds, int companyId, int userId)
    {
        return dptIds.Select(deptId => new UserCompanyDepartment
        {
            UserId = userId,
            CompanyId = companyId,
            DepartmentId = deptId
        }).ToList();
    }
}