using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Models.DocumentRecords;

namespace SWSA.MvcPortal.Models.Submissions;

public class CompanyStrikeOffSubmissionVM
{

    public int SubmissionId { get; set; }
    public int WorkAssignmentId { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string RegistrationNumber { get; set; }
    public CompanyType CompanyType { get; set; }
    public DateTime? IncorporationDate { get; set; }
    public MonthOfYear? YearEndMonth { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public DateTime? SSMSubmissionDate { get; set; }
    public DateTime? IRBSubmissionDate { get; set; }
    public DateTime? SSMStrikeOffDate { get; set; }
    public string? Remarks { get; set; }

    public List<DocumentRecordVM> Documents { get; set; }   
}
