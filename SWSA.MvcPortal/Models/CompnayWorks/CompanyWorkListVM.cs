using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkListVM
{
    public int TaskId { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public WorkType WorkType { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public CompanyActivityLevel ActivitySize { get; set; }
    public int YearToDo { get; set; }
    public MonthOfYear MonthToDo { get; set; }
    public WorkProgressStatus Status { get; set; }
    public string? InternalNote { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = null!;
    public int StaffId { get; set; }
    public string StaffName { get; set; } = null!;
}
