using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.Companies;
public class CompanyEditPageVM
{
    public Company Company { get; set; }
    public List<MsicCode> MsicCodes { get; set; } = [];
    public CompanyEditPageVM(Company cp, List<MsicCode> msicCodes)
    {
        Company = cp;
        MsicCodes = msicCodes;
    }
}

