
using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.ValueOfObject;

namespace SWSA.MvcPortal.Entities;

[Module("Submission")]
public class AuditSubmission : BaseSubmission
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


    public override bool IsSubmissionComplete()
    {
        if (!DateSubmitted.HasValue || !AccDueDate.HasValue) return false;

        return true;
    }

    public void SetTargetedCirculation(MonthOfYear? yearEndMonth)
    {
        if (!yearEndMonth.HasValue)
        {
            return;
        }

        int year = DateTime.Now.Year;
        int month = (int)yearEndMonth;
        DateTime yearEnd = new DateTime(year, month, 1);
        // Set TargettedCirculation to 7 months after the year end date
        TargetedCirculation = yearEnd.AddMonths(7);

    }
}
