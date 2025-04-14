using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkAssignmentCreatePageVM
{
    public Company Company { get; set; }

    public CompanyWorkAssignmentCreatePageVM(Company company)
    {
        Company = company;
    }
}
