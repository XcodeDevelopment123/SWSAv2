using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Dtos.Requests.Users;

public class CreateUserCompanyDepartmentRequest
{
    public string StaffId { get; set; }
    public List<int> DepartmentIds { get; set; } = new();
}

public static class UserCompanyDepartmentMapper
{
    public static List<UserCompanyDepartment> ToEntities(CreateUserCompanyDepartmentRequest dto, int companyId, int userId)
    {
        return dto.DepartmentIds.Select(deptId => new UserCompanyDepartment
        {
            UserId = userId,
            CompanyId = companyId,
            DepartmentId = deptId
        }).ToList();
    }
}