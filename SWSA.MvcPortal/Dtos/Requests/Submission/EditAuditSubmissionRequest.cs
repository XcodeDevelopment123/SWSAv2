namespace SWSA.MvcPortal.Dtos.Requests.Submission;

public class EditAuditSubmissionRequest
{
    public int CompanyId { get; set; }
    public int SubmissionId { get; set; }
    public DateTime? FirstYearAccountStart { get; set; }
    public DateTime? AccDueDate { get; set; }
    public DateTime? DateSubmitted { get; set; }
    public DateTime? TargettedCirculation { get; set; }
    public string? ReasonForLate { get; set; }
}
