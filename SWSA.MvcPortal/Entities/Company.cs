using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;

public class Company
{
    [Key]
    public int Id { get; set; }
    public string? FileNo { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? RegistrationNumber { get; set; }
    public string? EmployerNumber { get; set; } // E Number
    public string? TaxIdentificationNumber { get; set; } // TIN
    public MonthOfYear? YearEndMonth { get; set; }
    public DateTime? IncorporationDate { get; set; }
    public CompanyStatus Status { get; set; }
    public bool IsDeleted { get; set; }

    [ForeignKey(nameof(CompanyType))]
    public int CompanyTypeId { get; set; }
    public CompanyType CompanyType { get; set; } = null!;
    public ICollection<CompanyOwner> CompanyOwners { get; set; } = new List<CompanyOwner>();
    public ICollection<CompanyCommunicationContact> CommunicationContacts { get; set; } = new List<CompanyCommunicationContact>();
    public ICollection<CompanyOfficialContact> OfficialContacts { get; set; } = new List<CompanyOfficialContact>();
    public ICollection<CompanyDepartment> Departments { get; set; } = new List<CompanyDepartment>();
    public ICollection<CompanyMsicCode> MsicCodes { get; set; } = new List<CompanyMsicCode>();

    public string GetYearEndMonthLabel()
    {
        if (!YearEndMonth.HasValue || !Enum.IsDefined(typeof(MonthOfYear), YearEndMonth))
            return AppSettings.NotAvailable;

        var monthNum = ((int)YearEndMonth).ToString("D2");
        var monthName = YearEndMonth.ToString();
        return $"{monthNum} ({monthName})";
    }

}
