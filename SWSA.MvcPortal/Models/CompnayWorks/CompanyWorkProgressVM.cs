using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkProgressVM
{
    public int ProgressId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? TimeTakenInDays => StartDate.HasValue && EndDate.HasValue ? (EndDate.Value - StartDate.Value).Days : null;
    public WorkProgressStatus Status { get; set; }
    public string? ProgressNote { get; set; }
}
