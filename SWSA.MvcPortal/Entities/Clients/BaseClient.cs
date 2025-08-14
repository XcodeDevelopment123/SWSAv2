using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Extensions;
using SWSA.MvcPortal.Entities.Contacts;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities.Clients;

public abstract class BaseClient
{
    [Key]
    public int Id { get; set; }

    [SystemAuditLog("Group")]
    public string? Group { get; set; }    //User input

    [SystemAuditLog("Referral")]
    public string? Referral { get; set; }    //User input

    [SystemAuditLog("Name")]
    public string Name { get; set; } = null!;

    [SystemAuditLog("Year-End Month")]
    public MonthOfYear? YearEndMonth { get; set; } //Last Day for the month

    [SystemAuditLog("Tax Identification Number")]
    public string? TaxIdentificationNumber { get; set; } // TIN

    public ClientType ClientType { get; set; }

    public bool IsActive { get; set; }  

    public virtual ICollection<OfficialContact> OfficialContacts { get; set; } = [];
    public virtual ICollection<CommunicationContact> CommunicationContacts { get; set; } = [];

    public void Deactivate()
    {
        IsActive = false;   
    }

    public void Activate()
    {
        IsActive = true;
    }

    public string GetYearEnd()
    {
        if (!YearEndMonth.HasValue)
            return string.Empty;

        int month = (int)YearEndMonth.Value;
        int year = DateTime.Now.Year;

        if (DateTime.Now.Month > month)
            year++;

        var yearEndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
        return yearEndDate.ToFormattedString(DateTimeFormatType.MonthAndYearOnly);
    }
}
