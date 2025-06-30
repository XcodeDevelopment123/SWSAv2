using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.Companies;

public class SecretaryCompanyListVM
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public CompanyType CompanyType { get; set; }

    public MonthOfYear? YearEndMonth { get; set; }

    public int ForYear { get; set; }

    public DateTime? DateOfAnnualReturn { get; set; }//From AR Submission
    public DateTime? DateOfARDue { get; set; }//From AR Submission
    public DateTime? DateOfARSubmitted { get; set; }//From AR Submission
    public DateTime? DateOfAccSubmitted { get; set; }//From Audit Submission
    public DateTime? DateSentToClient { get; set; } //From AR Submission
    public DateTime? DateReturnedByClient { get; set; }//From AR Submission
    public string Remarks { get; set; } = null!;
}
