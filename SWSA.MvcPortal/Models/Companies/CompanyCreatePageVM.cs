using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.Companies;
public class CompanyCreatePageVM
{
    public List<MsicCode> MsicCodes { get; set; } = [];
    public CompanyType CompanyType { get; set; }

    public CompanyCreatePageVM(List<MsicCode> msicCodes, CompanyType type)
    {
        MsicCodes = msicCodes;
        CompanyType = type;
    }
}

