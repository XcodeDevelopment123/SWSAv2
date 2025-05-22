using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class EditCompanyWorkAssignmentRequest
{
    public int TaskId { get; set; }
    public CompanyStatus CompanyStatus { get; set; }
    public WorkType WorkType { get; set; }
    public bool IsYearEndTask { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public CompanyActivityLevel CompanyActivityLevel { get; set; }
    public DateTime? SsmExtensionDate { get; set; }
    public DateTime? AGMDate { get; set; } //Annual General Meeting
    public DateTime? ARDueDate { get; set; } //Annual Return Due Date
    public DateTime? ReminderDate { get; set; }
    public string? InternalNote { get; set; }
    public List<MonthOfYear> AuditMonths { get; set; }
    public List<MonthOfYear> AccountMonths { get; set; }
}
