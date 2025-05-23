using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.CompanyStrikeOffSubmission;

public class CompanyStrikeOffSubmissionVM
{
    public string SubmissionId { get; set; }
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
}
