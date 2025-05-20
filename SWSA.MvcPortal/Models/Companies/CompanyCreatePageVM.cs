using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.Companies;
public class CompanyCreatePageVM
{
    public List<MsicCode> MsicCodes { get; set; } = [];
    public List<UserSelectionVM> Users { get; set; } = [];

    public CompanyCreatePageVM(List<MsicCode> msicCodes, List<UserSelectionVM> users)
    {
        MsicCodes = msicCodes;
        Users = users;  
    }
}

