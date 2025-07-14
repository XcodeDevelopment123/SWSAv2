using SWSA.MvcPortal.Entities.WorkAssignments;
using SWSA.MvcPortal.Services.Interfaces.WorkAssignments;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class EditStrikeOffWorkRequest : IWorkAssignmentUpdater<StrikeOffWorkAssignment>
{
    public int CompanyId { get; set; }
    public int TaskId { get; set; }
    public DateTime? CompleteDate { get; set; }
    public DateTime? SSMSubmissionDate { get; set; }
    public DateTime? IRBSubmissionDate { get; set; }
    public DateTime? SSMStrikeOffDate { get; set; }

    public void ApplyTo(StrikeOffWorkAssignment entity)
    {
        entity.CompleteDate = CompleteDate;
        entity.SSMStrikeOffDate = SSMStrikeOffDate;
        entity.SSMSubmissionDate = SSMSubmissionDate;
        entity.IRBSubmissionDate = IRBSubmissionDate;
    }
}
