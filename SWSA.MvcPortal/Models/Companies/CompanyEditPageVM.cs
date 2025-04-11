using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Companies;
public class CompanyEditPageVM
{
    public Company Company { get; set; }
    public List<MsicCode> MsicCodes { get; set; } = [];
    public List<CompanyType> CompanyType { get; set; } = [];
    public List<Department> Departments { get; set; } = [];

    public CompanyEditPageVM(Company cp, List<MsicCode> msicCodes, List<CompanyType> companyType,List<Department> departments)
    {
        Company = cp;
        MsicCodes = msicCodes;
        CompanyType = companyType;
        Departments = departments;
    }
}

