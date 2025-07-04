
using SWSA.MvcPortal.Entities.WorkAssignments;
using SWSA.MvcPortal.Services.Interfaces.WorkAssignment;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class EditLLPWorkRequest : IWorkAssignmentUpdater<LLPWorkAssignment>
{
    public int CompanyId { get; set; }
    public int TaskId { get; set; }
    public DateTime? SSMExtensionDateForAcc { get; set; }

    public DateTime? ARDueDate { get; set; }

    public DateTime? AccountSubmitDate { get; set; }

    public DateTime? ARSubmitDate { get; set; }
    public DateTime? DateSentToClient { get; set; }

    public DateTime? DateReturnedByClient { get; set; }

    public void ApplyTo(LLPWorkAssignment entity)
    {
        entity.SSMExtensionDateForAcc = SSMExtensionDateForAcc;
        entity.ARDueDate = ARDueDate;
        entity.AccountSubmitDate = AccountSubmitDate;
        entity.ARSubmitDate = ARSubmitDate;
        entity.DateSentToClient = DateSentToClient;
        entity.DateReturnedByClient = DateReturnedByClient;
    }
}
