
namespace SWSA.MvcPortal.Models.Submissions;

public class AnnualReturnSubmissionVM : BaseSubmissionVM
{
    public int Year { get; set; }
    public DateTime? AnniversaryDate { get; set; } 
    public DateTime? TargetedARDate { get; set; } 
    public DateTime? DateOfAnnualReturn { get; set; }
    public DateTime? DateSubmitted { get; set; }
    public DateTime? DateSentToClient { get; set; }
    public DateTime? DateReturnedByClient { get; set; }
    public string? Remarks { get; set; }
}
