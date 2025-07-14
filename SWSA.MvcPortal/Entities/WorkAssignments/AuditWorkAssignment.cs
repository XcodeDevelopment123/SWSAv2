using SWSA.MvcPortal.Commons.Attributes;

namespace SWSA.MvcPortal.Entities.WorkAssignments;

public class AuditWorkAssignment : WorkAssignment
{
    [SystemAuditLog("First Year Account Start Date")]
    public DateTime? FirstYearAccountStart { get; set; }
    [SystemAuditLog("Account Due Date")]
    public DateTime? AccDueDate { get; set; }
    [SystemAuditLog("Date Submitted")]
    public DateTime? DateSubmitted { get; set; }
    [SystemAuditLog("Targeted Circulation (Month/Year)")]
    public DateTime? TargetedCirculation { get; set; } //Month and year only
    [SystemAuditLog("Is Late Submission")]
    public bool IsLate { get; set; } = false; // DateSubmitted > AccDueDate
    [SystemAuditLog("Reason For Late Submission")]
    public string? ReasonForLate { get; set; }

    public override bool IsComplete()
    {
        if (!DateSubmitted.HasValue || !AccDueDate.HasValue) return false;

        return true;
    }

    public string GetIsLateDisplyLabel()
    {
        if (DateSubmitted.HasValue && AccDueDate.HasValue)
        {
            return IsLate ? "Late" : "On Time";
        }
        return "";

    }
}
