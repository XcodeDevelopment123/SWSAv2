using SWSA.MvcPortal.Commons.Attributes;

namespace SWSA.MvcPortal.Entities.WorkAssignments;

public class LLPWorkAssignment : CompanyWorkAssignment
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
    public override bool IsComplete()
    {
        if (!AccountSubmitDate.HasValue || !ARSubmitDate.HasValue) return false;

        return true;
    }
}
