using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Companies;

public class EditCompanyRequest
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string? RegistrationNumber { get; set; }
    public string? EmployerNumber { get; set; }
    public string? TaxIdentificationNumber { get; set; }
    public MonthOfYear? YearEndMonth { get; set; }  // Year End Month (optional)
    public DateTime? IncorporationDate { get; set; } // Incorporation Date (optional) - 成立日期
    public CompanyStatus Status { get; set; }
    public int CompanyTypeId { get; set; }
    public List<int> MsicCodeIds { get; set; } = [];
}
