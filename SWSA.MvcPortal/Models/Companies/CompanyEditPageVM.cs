using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Companies;
public class CompanyEditPageVM
{
    public Company Company { get; set; }
    public List<MsicCode> MsicCodes { get; set; } = [];
    public List<Department> Departments { get; set; } = [];

    public CompanyEditPageVM(Company cp, List<MsicCode> msicCodes,List<Department> departments)
    {
        Company = cp;
        MsicCodes = msicCodes;
        Departments = departments;
    }
}

