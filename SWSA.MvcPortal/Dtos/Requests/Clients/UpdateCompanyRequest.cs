using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Clients;

public class UpdateCompanyRequest
{
    public int ClientId { get; set; }   
    public string CompanyName { get; set; } = null!;
    public string RegistrationNumber { get; set; }
    public string? EmployerNumber { get; set; }
    public string? TaxIdentificationNumber { get; set; }
    public MonthOfYear? YearEndMonth { get; set; }
    public DateTime? IncorporationDate { get; set; }
    public ClientType ClientType { get; set; }
    public CompanyActivityLevel ActivitySize { get; set; }
    public CompanyType? CompanyType { get; set; }
    public CompanyStatus? CompanyStatus { get; set; }
    public string? CompanyStatusReason { get; set; }
    public CreditRating? CreditRating { get; set; }
    public string? ClientRating { get; set; }
    public string? BusinessNature { get; set; }
    public string? ServiceSelected { get; set; }
    public string? PrincipalActivity { get; set; }
    public bool? ForeignOwned { get; set; }
    public string? AppointmentEngagementData { get; set; }
    public List<int>? MsicCodeIds { get; set; }
    public ClientCategoryRequest CategoryInfo { get; set; }
}
