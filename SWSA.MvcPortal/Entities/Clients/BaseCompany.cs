using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Entities.Clients;

public abstract class BaseCompany : BaseClient
{
    [SystemAuditLog("File Number")]
    public string? FileNo { get; set; } = null!; //User input

    [SystemAuditLog("Registration Number")]
    public string RegistrationNumber { get; set; } = null!;

    [SystemAuditLog("Company Type")]
    public CompanyType CompanyType { get; set; }

    [SystemAuditLog("Incorporation Date")]
    public DateTime? IncorporationDate { get; set; }


    [SystemAuditLog("Employer Number")]
    public string? EmployerNumber { get; set; } // E Number
    public virtual ICollection<CompanyOwner> Owners { get; set; } = [];
    public virtual ICollection<CompanyMsicCode> MsicCodes { get; set; } = [];
}
