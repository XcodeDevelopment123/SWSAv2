using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Models.CompnayWorks;
using SWSA.MvcPortal.Models.SystemAuditLogs;

namespace SWSA.MvcPortal.Models.Users;

public class UserOverviewVM : UserVM
{
    public List<SystemAuditLogListVM> AuditLogs { get; set; } = [];
    public List<CompanyListVM> AssignedCompanies { get; set; } = [];
    public List<CompanyWorkListVM> AssignedWorks { get; set; } = [];

    public void MergeCompany()
    {
        AssignedCompanies = AssignedCompanies.DistinctBy(x => x.CompanyId).ToList();
    }
}
