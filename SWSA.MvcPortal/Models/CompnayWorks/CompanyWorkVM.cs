using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkVM
{
    public int TaskId { get; set; }
    public WorkType WorkType { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public CompanyActivityLevel ActivitySize { get; set; }
    public DateTime DueDate { get; set; }
    public int YearToDo => DueDate.Year;
    public MonthOfYear MonthToDo => (MonthOfYear)DueDate.Month;
    public string? InternalNote { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime? CompletedDate { get; set; }
    public CompanyWorkProgressVM Progress { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public UserVM User { get; set; } = null!;
}
