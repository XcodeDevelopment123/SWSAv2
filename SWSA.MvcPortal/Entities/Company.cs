using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Filters;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

public class Company
{
    [Key]
    public int Id { get; set; }

    [SystemAuditLog("File Number")]
    public string? FileNo { get; set; } = null!;

    [SystemAuditLog("Company Name")]
    public string Name { get; set; } = null!;

    [SystemAuditLog("Registration Number")]
    public string? RegistrationNumber { get; set; }

    [SystemAuditLog("Employer Number")]
    public string? EmployerNumber { get; set; } // E Number

    [SystemAuditLog("Tax Identification Number")]
    public string? TaxIdentificationNumber { get; set; } // TIN

    [SystemAuditLog("Year-End Month")]
    public MonthOfYear? YearEndMonth { get; set; }

    [SystemAuditLog("Incorporation Date")]
    public DateTime? IncorporationDate { get; set; }

    [SystemAuditLog("Company Status")]
    public CompanyStatus Status { get; set; }

    [SystemAuditLog("Company Type")]
    public CompanyType CompanyType { get; set; }
    public bool IsDeleted { get; set; }
    public CompanyComplianceDate CompanyComplianceDate { get; set; } = null!;
    public ICollection<CompanyOwner> CompanyOwners { get; set; } = new List<CompanyOwner>();
    public ICollection<CompanyStaff> CompanyStaffs { get; set; } = new List<CompanyStaff>();
    public ICollection<CompanyOfficialContact> OfficialContacts { get; set; } = new List<CompanyOfficialContact>();
    public ICollection<CompanyDepartment> Departments { get; set; } = new List<CompanyDepartment>();
    public ICollection<CompanyMsicCode> MsicCodes { get; set; } = new List<CompanyMsicCode>();
    public virtual ICollection<SystemAuditLog> SystemAuditLogs { get; set; } = new List<SystemAuditLog>();
    public virtual ICollection<UserCompanyDepartment> UserCompanyDepartments { get; set; } = new List<UserCompanyDepartment>();

}
