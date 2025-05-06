using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.Companies;
public class CompanyCreatePageVM
{
    public List<MsicCode> MsicCodes { get; set; } = [];
    public List<Department> Departments { get; set; } = [];
    public List<UserSelectionVM> Users { get; set; } = [];

    public CompanyCreatePageVM(List<MsicCode> msicCodes, List<Department> departments,List<UserSelectionVM> users)
    {
        MsicCodes = msicCodes;
        Departments = departments;
        Users = users;  
    }
}

