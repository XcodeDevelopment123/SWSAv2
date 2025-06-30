using SWSA.MvcPortal.Models.DocumentRecords;

namespace SWSA.MvcPortal.Models.Submissions;

public class CompanyStrikeOffSubmissionVM : BaseSubmissionVM
{
    public DateTime? StartDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public DateTime? SSMSubmissionDate { get; set; }
    public DateTime? IRBSubmissionDate { get; set; }
    public DateTime? SSMStrikeOffDate { get; set; }
    public string? Remarks { get; set; }

    public override string DisplayYearLabel => $"{ForYear}";
}
