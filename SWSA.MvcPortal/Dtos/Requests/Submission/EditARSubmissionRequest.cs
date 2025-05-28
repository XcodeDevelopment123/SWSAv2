
namespace SWSA.MvcPortal.Dtos.Requests.Submission;

public class EditARSubmissionRequest
{
    public int CompanyId { get; set; }
    public int SubmissionId { get; set; }
    public DateTime? AnniversaryDate { get; set; } //EST 17 months from  incopr date
    public DateTime? ARDueDate { get; set; }
    public DateTime? TargetedARDate { get; set; } // 7 Month from year end
    public DateTime? DateOfAnnualReturn { get; set; }
    public DateTime? DateSubmitted { get; set; }
    public DateTime? DateSentToClient { get; set; }
    public DateTime? DateReturnedByClient { get; set; }
    public string? Remarks { get; set; }
}
