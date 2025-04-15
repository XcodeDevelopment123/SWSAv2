using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class EditCompanyWorkProgressRequest
{
    public int ProgressId { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; } 
    public int TimeTakenInDays { get; set; }
    public WorkProgressStatus Status { get; set; }
    public string? ProgressNote { get; set; } = null!;
}
