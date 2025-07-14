using SWSA.MvcPortal.Entities.WorkAssignments;
using SWSA.MvcPortal.Services.Interfaces.WorkAssignments;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class EditARWorkRequest : IWorkAssignmentUpdater<AnnualReturnWorkAssignment>
{
    public int CompanyId { get; set; }
    public int TaskId { get; set; }
    public DateTime? AnniversaryDate { get; set; } //EST 17 months from  incopr date
    public DateTime? ARDueDate { get; set; }
    public DateTime? TargetedARDate { get; set; } // 7 Month from year end
    public DateTime? DateOfAnnualReturn { get; set; }
    public DateTime? DateSubmitted { get; set; }
    public DateTime? DateSentToClient { get; set; }
    public DateTime? DateReturnedByClient { get; set; }

    public void ApplyTo(AnnualReturnWorkAssignment entity)
    {
        entity.AnniversaryDate = AnniversaryDate;
        entity.ARDueDate = ARDueDate;
        entity.TargetedARDate = TargetedARDate;
        entity.DateOfAnnualReturn = DateOfAnnualReturn;
        entity.DateSubmitted = DateSubmitted;
        entity.DateSentToClient = DateSentToClient;
        entity.DateReturnedByClient = DateReturnedByClient;
    }
}
