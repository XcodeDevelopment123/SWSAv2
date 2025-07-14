using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.Companies;
public class CompanyEditPageVM
{
    public List<MsicCode> MsicCodes { get; set; } = [];
    public CompanyEditPageVM( List<MsicCode> msicCodes)
    {
        MsicCodes = msicCodes;
    }
}

