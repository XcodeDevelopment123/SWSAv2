using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Dtos.Requests.Users;

public class CreateUserCompanyDepartmentRequest
{
    public string StaffId { get; set; }
    public List<string> Departments { get; set; } = new();
    public int? CompanyId { get; set; }
}

public static class UserCompanyDepartmentMapper
{
    public static List<UserCompanyDepartment> ToEntities(List<string> dpts, int companyId, int userId)
    {
        return [.. dpts.Select(deptId => new UserCompanyDepartment
        {
            UserId = userId,
            CompanyId = companyId,
            Department = deptId
        })];
    }
}