using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.SystemAuditLogs;

namespace SWSA.MvcPortal.Models.Users;

public class UserOverviewVM : UserVM
{
    public List<SystemAuditLogListVM> AuditLogs { get; set; } = [];
    public List<Company> AssignedCompanies { get; set; } = [];
    public List<CompanyWorkAssignment> AssignedWorks { get; set; } = [];

    public void MergeCompany()
    {
        AssignedCompanies = AssignedCompanies.DistinctBy(x => x.Id).ToList();
    }
}
