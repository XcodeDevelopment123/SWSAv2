using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.Companies;
public class CompanyEditPageVM
{
    public Company Company { get; set; }
    public List<MsicCode> MsicCodes { get; set; } = [];
    public List<Department> Departments { get; set; } = [];
    public List<UserSelectionVM> Users { get; set; } = [];
    public CompanyEditPageVM(Company cp, List<MsicCode> msicCodes, List<Department> departments, List<UserSelectionVM> users)
    {
        Company = cp;
        MsicCodes = msicCodes;
        Departments = departments;
        Users = users;
    }
}

