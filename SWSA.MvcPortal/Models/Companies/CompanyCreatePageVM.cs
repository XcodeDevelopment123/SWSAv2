using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.Systems;

namespace SWSA.MvcPortal.Models.Companies;
public class CompanyCreatePageVM
{
    public List<MsicCode> MsicCodes { get; set; } = [];

    public CompanyCreatePageVM(List<MsicCode> msicCodes)
    {
        MsicCodes = msicCodes;
    }
}

