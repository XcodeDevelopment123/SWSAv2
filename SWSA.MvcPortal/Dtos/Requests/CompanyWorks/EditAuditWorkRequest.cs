using SWSA.MvcPortal.Entities.WorkAssignments;
using SWSA.MvcPortal.Services.Interfaces.WorkAssignments;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class EditAuditWorkRequest : IWorkAssignmentUpdater<AuditWorkAssignment>
{
    public int TaskId { get; set; }
    public int CompanyId { get; set; }
    public DateTime? FirstYearAccountStart { get; set; }
    public DateTime? AccDueDate { get; set; }
    public DateTime? DateSubmitted { get; set; }
    public DateTime? TargetedCirculation { get; set; }
    public string? ReasonForLate { get; set; }

    public void ApplyTo(AuditWorkAssignment entity)
    {
        entity.AccDueDate = AccDueDate;
        entity.DateSubmitted = DateSubmitted;
        entity.ReasonForLate = ReasonForLate;
        entity.IsLate = AccDueDate.HasValue && DateSubmitted.HasValue && DateSubmitted > AccDueDate;
    }
}
