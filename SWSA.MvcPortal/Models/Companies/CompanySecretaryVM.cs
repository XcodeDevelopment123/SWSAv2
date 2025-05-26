using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Models.CompnayWorks;

namespace SWSA.MvcPortal.Models.Companies;

public class CompanySecretaryVM
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public MonthOfYear? YearEndMonth { get; set; }
    public CompanyType CompanyType { get; set; }
    public DateTime? IncorporationDate { get; set; }
    public string RegistrationNumber { get; set; } = null!;
    public string? EmployerNumber { get; set; }
    public string? TaxIdentificationNumber { get; set; }
    public bool IsStrikedOff { get; set; } = false;
    public StrikeOffStatus StrikeOffStatus { get; set; } = StrikeOffStatus.NotApplied;
    public DateTime? StrikeOffEffectiveDate { get; set; }
    public List<CompanyMsicCodeVM> MsicCodes { get; set; } = new List<CompanyMsicCodeVM>();
    public List<CompanyOwnerVM> Owners { get; set; } = new List<CompanyOwnerVM>();
    public List<CompanyWorkVM> WorkAssignments { get; set; } = new List<CompanyWorkVM>();
}
