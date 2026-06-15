using NuGet.Packaging;
using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.Contacts;
using System.ComponentModel.DataAnnotations.Schema;

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

    [SystemAuditLog("Activity Size")]
    public CompanyActivityLevel ActivitySize { get; set; }


    [SystemAuditLog("Employer Number")]
    public string? EmployerNumber { get; set; } // E Number

    [SystemAuditLog("Company Status")]
    public CompanyStatus? CompanyStatus { get; set; }

    [SystemAuditLog("Company Status Reason")]
    public string? CompanyStatusReason { get; set; }

    [SystemAuditLog("Credit Rating")]
    public CreditRating? CreditRating { get; set; }

    [SystemAuditLog("Client Rating")]
    public string? ClientRating { get; set; }

    [SystemAuditLog("Business Nature")]
    public string? BusinessNature { get; set; }

    [SystemAuditLog("Service Selected")]
    public string? ServiceSelected { get; set; }

    [SystemAuditLog("Principal Activity")]
    public string? PrincipalActivity { get; set; }

    [SystemAuditLog("Foreign Owned")]
    public bool? ForeignOwned { get; set; }

    [SystemAuditLog("Appointment Engagement")]
    public string? AppointmentEngagementData { get; set; }

    public virtual ICollection<CompanyOwner> Owners { get; set; } = [];
    public virtual ICollection<CompanyMsicCode> MsicCodes { get; set; } = [];

    [NotMapped]
    [SystemAuditLog("Msic Codes")]
    public string MsicCodesString
    {
        get => string.Join(", ", MsicCodes.Select(m => m.MsicCode?.Code));
        set { } // This setter is intentionally left empty to prevent modification
    }

    public void UpdateCompanyInfo(string name, string registrationNumber, CompanyActivityLevel activityLevel, string? tin, string? employerNumber, MonthOfYear? yearEnd, DateTime? incorpDate)
    {
        Name = name;
        RegistrationNumber = registrationNumber;
        ActivitySize = activityLevel;
        TaxIdentificationNumber = tin;
        EmployerNumber = employerNumber;
        YearEndMonth = yearEnd;
        IncorporationDate = incorpDate;
    }

    public void UpdateAdminInfo(string? group, string? referral, string? fileNo)
    {
        Group = group;
        Referral = referral;
        FileNo = fileNo;
    }

    public void SyncMsicCode(IEnumerable<int> requestedMsicCodeIds)
    {
        var limitIds = requestedMsicCodeIds.Where(id => id > 0).Take(3).ToHashSet();

        var existingIds = MsicCodes.Select(c => c.MsicCodeId).ToHashSet();

        var toAdd = limitIds.Except(existingIds).ToList();
        MsicCodes.AddRange(toAdd.Select(id => new CompanyMsicCode(id)));


        var toRemove = MsicCodes.Where(c => !limitIds.Contains(c.MsicCodeId)).ToList();
        foreach (var item in toRemove)
        {

            MsicCodes.Remove(item);
        }
    }
}
