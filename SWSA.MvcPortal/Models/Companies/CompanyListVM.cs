using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities;
using System.Text.Json.Serialization;

namespace SWSA.MvcPortal.Models.Companies;

public class CompanyListVM
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public string? EmployerNumber { get; set; } 
    public string? TaxIdentificationNumber { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MonthOfYear? YearEndMonth { get; set; }
    public DateTime? IncorporationDate { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CompanyStatus Status { get; set; }
    public CompanyType CompanyType { get; set; }
    public string CompanyDirectorName { get; set; } = null!; // First Owner + Director position name
    public int ContactsCount { get; set; }  // Communication Contacts + Official Contacts
    public int DepartmentsCount { get; set; }
    public int MsicCodesCount { get; set; }
    public List<CompanyMsicCode> MsicCodes { get; set; } = new List<CompanyMsicCode>();

}
