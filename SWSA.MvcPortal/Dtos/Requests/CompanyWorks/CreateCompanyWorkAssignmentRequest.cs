using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class CreateCompanyWorkAssignmentRequest
{
    public WorkType WorkType { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public int CompanyId { get; set; }
    public int CompanyDepartmentId { get; set; }
    public CompanyActivityLevel CompanyActivityLevel { get; set; }
    public DateTime DueDate { get; set; }
    public string AssignedStaffId { get; set; } = null!;
    public string? InternalNote { get; set; }
    public CreateCompanyWorkProgressRequest CompanyWorkProgressRequest { get; set; } = null!;
}
