using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.ValueOfObject;

namespace SWSA.MvcPortal.Entities;
[Module("Submission")]
public class LLPSubmission : BaseSubmission
{
    [SystemAuditLog("SSM Extension Date For Account")]
    public DateTime? SSMExtensionDateForAcc { get; set; }
    [SystemAuditLog("Annual Return Due Date")]

    public DateTime? ARDueDate { get; set; }
    [SystemAuditLog("Account Submit Date")]

    public DateTime? AccountSubmitDate { get; set; }
    [SystemAuditLog("Annual Return Submit Date")]

    public DateTime? ARSubmitDate { get; set; }
    [SystemAuditLog("Date Sent to Client")]
    public DateTime? DateSentToClient { get; set; }

    [SystemAuditLog("Date Returned by Client")]
    public DateTime? DateReturnedByClient { get; set; }
    public bool IsSubmissionComplete()
    {
        if (!AccountSubmitDate.HasValue || !ARSubmitDate.HasValue) return false;

        return true;
    }
}
