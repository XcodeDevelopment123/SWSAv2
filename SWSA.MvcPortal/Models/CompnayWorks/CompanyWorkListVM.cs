using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkListVM
{
    public int TaskId { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string CompanyRegistrationNumber { get; set; } = null!;
    public WorkType WorkType { get; set; }
    public CompanyStatus CompanyStatus { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public CompanyActivityLevel ActivitySize { get; set; }
    public WorkProgressStatus Status { get; set; }
    public List<CompanyWorkUserVM> AssignedUsers { get; set; } = new List<CompanyWorkUserVM>();
    public DateTime? ReminderDate { get; set; }
    public string? InternalNote { get; set; }
    public bool YearEndToDo { get; set; } = false;
}
