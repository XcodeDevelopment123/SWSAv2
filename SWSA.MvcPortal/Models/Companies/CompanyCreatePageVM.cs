using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Companies;
public class CompanyCreatePageVM
{
    public List<MsicCode> MsicCodes { get; set; } = [];
    public List<Department> Departments { get; set; } = [];

    public CompanyCreatePageVM(List<MsicCode> msicCodes, List<Department> departments)
    {
        MsicCodes = msicCodes;
        Departments = departments;
    }
}

