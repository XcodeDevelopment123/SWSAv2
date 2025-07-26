using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.SystemAuditLogs;

namespace SWSA.MvcPortal.Models.Users;

public class UserOverviewVM : UserVM
{
    public List<SystemAuditLogListVM> AuditLogs { get; set; } = [];
    public List<WorkAssignment> AssignedWorks { get; set; } = [];
}
