using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class CreateCompanyWorkProgressRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public WorkProgressStatus Status { get; set; } = WorkProgressStatus.Pending;
    public string? ProgressNote { get; set; }
}
