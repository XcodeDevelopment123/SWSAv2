namespace SWSA.MvcPortal.Models.Submissions;

public class AuditSubmissionVM : BaseSubmissionVM
{
    public DateTime? FirstYearAccountStart { get; set; }
    public DateTime? AccDueDate { get; set; }
    public DateTime? DateSubmitted { get; set; }
    public DateTime? TargetedCirculation { get; set; } //Month and year only
    public bool IsLate { get; set; } = false; // DateSubmitted > AccDueDate
    public string? ReasonForLate { get; set; }

    public override string DisplayYearLabel => $"{ForYear} - {ForYear + 1}";

    public string GetIsLateDisplyLabel()
    {
        if (DateSubmitted.HasValue && AccDueDate.HasValue)
        {
            return IsLate ? "Late" : "On Time";
        }
        return "";

    }
}
