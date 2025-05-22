using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Filters;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkVM
{
    public int TaskId { get; set; }
    public CompanyStatus CompanyStatus { get; set; }
    public WorkType WorkType { get; set; }
    public CompanyActivityLevel ActivitySize { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public string? InternalNote { get; set; }
    public bool IsYearEndTask { get; set; }
    public DateTime? SsmExtensionDate { get; set; }
    public DateTime? AGMDate { get; set; } //Annual General Meeting
    public DateTime? ARDueDate { get; set; } //Annual Return Due Date
    [SystemAuditLog("Reminder Date")]
    public DateTime? ReminderDate { get; set; }
    public CompanyWorkProgressVM Progress { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public UserVM User { get; set; } = null!;
    public List<CompanyWorkUserVM> AssignedUsers { get; set; } = new List<CompanyWorkUserVM>();
    public List<CompanyWorkMonthVM> AccountPlannedMonths { get; set; } = new List<CompanyWorkMonthVM>();
    public List<CompanyWorkMonthVM> AuditPlannedMonths { get; set; } = new List<CompanyWorkMonthVM>();


    public void MergeUsers()
    {
        var mergedResult = AssignedUsers
  .GroupBy(x => x.StaffId)
  .Select(g =>
  {
      var first = g.First();
      return new CompanyWorkUserVM
      {
          StaffId = first.StaffId,
          StaffName = first.StaffName,
          Role = first.Role,
          IsAssignedToAccount = g.Any(x => x.IsAssignedToAccount),
          IsAssignedToAudit = g.Any(x => x.IsAssignedToAudit)
      };
  }).ToList();

        AssignedUsers = mergedResult;
    }
}
