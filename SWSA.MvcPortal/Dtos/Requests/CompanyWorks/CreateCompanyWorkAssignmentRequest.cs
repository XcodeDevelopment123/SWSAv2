using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class CreateCompanyWorkAssignmentRequest
{
    public WorkType WorkType { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public int CompanyId { get; set; }
    public CompanyActivityLevel CompanyActivityLevel { get; set; }
    public int YearToDo { get; set; }  // 2025, 2026
    public MonthOfYear MonthToDo { get; set; } // 1-12
    public string AssignedStaffId { get; set; } = null!;
    public string? InternalNote { get; set; }
    public CreateCompanyWorkProgressRequest CompanyWorkProgressRequest { get; set; } = null!;
}
