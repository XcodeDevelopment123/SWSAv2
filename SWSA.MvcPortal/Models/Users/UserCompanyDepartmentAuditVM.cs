using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Users;

public class UserCompanyDepartmentAuditVM
{
    [SystemAuditLog("Departments")]
    public string Departments { get; set; }

    public UserCompanyDepartmentAuditVM(List<string> departments)
    {
        Departments = string.Join(", ", departments.Distinct());
    }
}
