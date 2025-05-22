using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkAssignmentCreatePageVM
{
    public Company Company { get; set; }
    public List<UserSelectionVM> AccountingUserSelections { get; set; }
    public List<UserSelectionVM> AuditUserSelections { get; set; }
    public CompanyWorkAssignmentCreatePageVM(Company company, List<UserSelectionVM> users)
    {
        Company = company;
        AccountingUserSelections = [.. users
        .Where(u => u.CompanyDepartments != null
                    && u.CompanyDepartments.TryGetValue(company.Id, out var depts)
                    && depts.Contains(DepartmentType.Account))];

        AuditUserSelections = [.. users
        .Where(u => u.CompanyDepartments != null
                    && u.CompanyDepartments.TryGetValue(company.Id, out var depts)
                    && depts.Contains(DepartmentType.Audit))];
    }
}
