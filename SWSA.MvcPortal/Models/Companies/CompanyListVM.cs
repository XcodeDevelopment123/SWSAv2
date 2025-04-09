using SWSA.MvcPortal.Commons.Enums;
using System.Text.Json.Serialization;

namespace SWSA.MvcPortal.Models.Companies;

public class CompanyListVM
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MonthOfYear? YearEndMonth { get; set; }
    public DateTime? IncorporationDate { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CompanyStatus Status { get; set; }
    public string CompanyType { get; set; } = null!;
    public string CompanyDirectorName { get; set; } = null!; // First Owner + Director position name
    public int ContactsCount { get; set; }  // Communication Contacts + Official Contacts
    public int DepartmentsCount { get; set; }
    public int MsicCodesCount { get; set; }
}
