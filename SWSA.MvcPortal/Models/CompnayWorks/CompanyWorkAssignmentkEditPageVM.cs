using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkAssignmentkEditPageVM
{
    public CompanyWorkVM CompanyWork { get; set; } = null!;
    public CompanySimpleInfoVM Company { get; set; } = null!;
    public List<UserSelectionVM> UserSelections { get; set; }

    public CompanyWorkAssignmentkEditPageVM(CompanyWorkVM companyWork,CompanySimpleInfoVM cpInfo, List<UserSelectionVM> users)
    {
        CompanyWork = companyWork;
        Company = cpInfo;
        UserSelections = [.. users];
    }
}
