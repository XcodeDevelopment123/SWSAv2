using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkAssignmentkEditPageVM
{
    public CompanyWorkVM CompanyWork { get; set; } = null!;
    public List<UserSelectionVM> AccountingUserSelections { get; set; }
    public List<UserSelectionVM> AuditUserSelections { get; set; }

    public CompanyWorkAssignmentkEditPageVM(CompanyWorkVM companyWork, List<UserSelectionVM> users)
    {
        CompanyWork = companyWork;
        AccountingUserSelections = [.. users
            .Where(u => u.CompanyDepartments != null
                    && u.CompanyDepartments.TryGetValue(companyWork.Company.Id, out var depts)
                    && depts.Contains(DepartmentType.Account))];

        AuditUserSelections = [.. users
            .Where(u => u.CompanyDepartments != null
                    && u.CompanyDepartments.TryGetValue(companyWork.Company.Id, out var depts)
                    && depts.Contains(DepartmentType.Audit))];
    }
}
