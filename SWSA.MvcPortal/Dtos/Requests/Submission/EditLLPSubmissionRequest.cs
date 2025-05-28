using SWSA.MvcPortal.Commons.Attributes;

namespace SWSA.MvcPortal.Dtos.Requests.Submission;

public class EditLLPSubmissionRequest
{
    public int CompanyId { get; set; }
    public int SubmissionId { get; set; }
    public DateTime? SSMExtensionDateForAcc { get; set; }

    public DateTime? ARDueDate { get; set; }

    public DateTime? AccountSubmitDate { get; set; }

    public DateTime? ARSubmitDate { get; set; }
    public DateTime? DateSentToClient { get; set; }

    public DateTime? DateReturnedByClient { get; set; }
    public string? Remarks { get; set; }

}
