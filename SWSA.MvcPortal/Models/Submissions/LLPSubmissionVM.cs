using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.Submissions;

public class LLPSubmissionVM : BaseSubmissionVM
{
    public CompanyStatus CompanyStatus { get; set; } // Dormant, Active, etc
    public CompanyActivityLevel CompanyActivityLevel { get; set; }
    public DateTime? SSMExtensionDateForAcc { get; set; }
    public DateTime? ARDueDate { get; set; }
    public DateTime? AccountSubmitDate { get; set; }
    public DateTime? ARSubmitDate { get; set; }
    public DateTime? DateSentToClient { get; set; }
    public DateTime? DateReturnedByClient { get; set; }
}
