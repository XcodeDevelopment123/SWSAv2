using SWSA.MvcPortal.Commons.Attributes;

namespace SWSA.MvcPortal.Entities.WorkAssignments;

public class AnnualReturnWorkAssignment : CompanyWorkAssignment
{
    [SystemAuditLog("Anniversary Date")]
    public DateTime? AnniversaryDate { get; set; } //EST 17 months from  incopr date
    [SystemAuditLog("Annual Return Due Date")]
    public DateTime? ARDueDate { get; set; }

    [SystemAuditLog("Targeted AR Date")]
    public DateTime? TargetedARDate { get; set; } // 7 Month from year end

    [SystemAuditLog("Actual Date of Annual Return")]
    public DateTime? DateOfAnnualReturn { get; set; }

    [SystemAuditLog("Actual Date Of AR Submitted")]
    public DateTime? DateSubmitted { get; set; }

    [SystemAuditLog("Date Sent to Client")]
    public DateTime? DateSentToClient { get; set; }

    [SystemAuditLog("Date Returned by Client")]
    public DateTime? DateReturnedByClient { get; set; }

    public override bool IsComplete()
    {
        if (!DateSubmitted.HasValue) return false;

        return true;
    }
}
