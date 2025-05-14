using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkAssignmentkEditPageVM
{
    public CompanyWorkVM CompanyWork { get; set; } = null!;
    public List<UserSelectionVM> UserSelections { get; set; }

    public CompanyWorkAssignmentkEditPageVM(CompanyWorkVM companyWork, List<UserSelectionVM> users)
    {
        CompanyWork = companyWork;
        UserSelections = users;
    }
}
