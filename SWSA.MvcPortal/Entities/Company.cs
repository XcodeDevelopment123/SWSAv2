using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

[Module("CompanyProfile")]
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

    [SystemAuditLog("Company Type")]
    public CompanyType CompanyType { get; set; }
    [SystemAuditLog("Company Status")]
    public CompanyStatus CompanyStatus { get; set; } // Dormant, Active, etc
    [SystemAuditLog("Company Activity Level")]
    public CompanyActivityLevel CompanyActivityLevel { get; set; }

    public bool IsDeleted { get; set; }

    public CompanyComplianceDate ComplianceDate { get; set; } = null!;
    public virtual ICollection<CompanyWorkAssignment> WorkAssignments { get; set; } = new List<CompanyWorkAssignment>();
    public virtual ICollection<CompanyOfficialContact> OfficialContacts { get; set; } = new List<CompanyOfficialContact>();
    public virtual ICollection<CompanyCommunicationContact> CommunicationContacts { get; set; } = new List<CompanyCommunicationContact>();
    public virtual ICollection<CompanyOwner> Owners { get; set; } = new List<CompanyOwner>();
    public virtual ICollection<DocumentRecord> DocumentRecords { get; set; } = new List<DocumentRecord>();
    public virtual ICollection<SystemAuditLog> SystemAuditLogs { get; set; } = new List<SystemAuditLog>();
    public virtual ICollection<CompanyMsicCode> MsicCodes { get; set; } = new List<CompanyMsicCode>();

    public string GetDirectorName()
    {
        if (this.Owners == null || this.Owners.Count == 0)
            return string.Empty;

        var director = this.Owners.FirstOrDefault(o => o.Position == PositionType.Director);
        return director?.NamePerIC ?? string.Empty;
    }

    public int GetContactCount()
    {
        return CommunicationContacts.Count + OfficialContacts.Count;
    }
}
