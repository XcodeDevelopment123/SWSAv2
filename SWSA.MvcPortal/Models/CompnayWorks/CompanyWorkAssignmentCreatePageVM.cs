using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkAssignmentCreatePageVM
{
    public CompanySimpleInfoVM Company { get; set; }
    public List<UserSelectionVM> UserSelections { get; set; }
    public CompanyWorkAssignmentCreatePageVM(CompanySimpleInfoVM company, List<UserSelectionVM> users)
    {
        Company = company;
        UserSelections = [.. users];

        ///Here is filter user by department
        //UserSelections = [.. users
        //.Where(u => u.CompanyDepartments != null
        //            && u.CompanyDepartments.TryGetValue(company.Id, out var depts)
        //            && depts.Contains(DepartmentType.Audit))];
    }
}
