using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Companies;
public class CompanyCreatePageVM
{
    public List<MsicCode> MsicCodes { get; set; } = [];
    public List<CompanyType> CompanyType { get; set; } = [];

    public CompanyCreatePageVM(List<MsicCode> msicCodes, List<CompanyType> companyType)
    {
        MsicCodes = msicCodes;
        CompanyType = companyType;
    }
}

