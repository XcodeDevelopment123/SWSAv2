using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkAssignmentCreatePageVM
{
    public Company Company { get; set; }
    public List<UserSelectionVM> UserSelections { get; set; }
    public CompanyWorkAssignmentCreatePageVM(Company company, List<UserSelectionVM> users)
    {
        Company = company;
        UserSelections = users;
    }
}
