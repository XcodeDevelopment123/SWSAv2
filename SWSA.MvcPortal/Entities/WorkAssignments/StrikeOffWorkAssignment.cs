using SWSA.MvcPortal.Commons.Attributes;

namespace SWSA.MvcPortal.Entities.WorkAssignments;

public class StrikeOffWorkAssignment : WorkAssignment
{
    [SystemAuditLog("Strike-Off Start Date")]
    public DateTime? StartDate { get; set; }

    [SystemAuditLog("Strike-Off Complete Date")]
    public DateTime? CompleteDate { get; set; }

    [SystemAuditLog("SSM Submission Date")]
    public DateTime? SSMSubmissionDate { get; set; }

    [SystemAuditLog("IRB Submission Date")]
    public DateTime? IRBSubmissionDate { get; set; }

    [SystemAuditLog("SSM Strike-Off Date")]
    public DateTime? SSMStrikeOffDate { get; set; }
    public override bool IsComplete()
    {
        if (CompleteDate.HasValue && SSMSubmissionDate.HasValue && IRBSubmissionDate.HasValue && SSMStrikeOffDate.HasValue)
        {
            return true;
        }
        return false;
    }
}
