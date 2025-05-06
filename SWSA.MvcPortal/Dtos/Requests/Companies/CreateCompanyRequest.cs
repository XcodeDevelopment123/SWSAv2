using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Users;

namespace SWSA.MvcPortal.Dtos.Requests.Companies;

public class CreateCompanyRequest
{
    public string CompanyName { get; set; } = null!;
    public string? RegistrationNumber { get; set; }
    public string? EmployerNumber { get; set; }
    public string? TaxIdentificationNumber { get; set; }
    public MonthOfYear? YearEndMonth { get; set; }  // Year End Month (optional)
    public DateTime? IncorporationDate { get; set; } // Incorporation Date (optional) - 成立日期
    public CompanyStatus Status { get; set; }
    public CompanyType CompanyType { get; set; }

    public CreateCompanyComplianceDate ComplianceDate { get; set; } = new();
    public List<int> MsicCodeIds { get; set; } = [];
    public List<int> DepartmentsIds { get; set; } = [];
    public List<CreateCompanyOwnerRequest> CompanyOwners { get; set; } = [];
    public List<CreateCompanyStaffRequest> StaffsContact { get; set; } = [];
    public List<CreateCompanyOfficialContactRequest> OfficialContacts { get; set; } = [];
    public List<CreateUserCompanyDepartmentRequest> HandleUsers { get; set; } = [];
}
