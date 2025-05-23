using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.Companies;

public class CompanyListVM
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public string? EmployerNumber { get; set; } 
    public string? TaxIdentificationNumber { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public MonthOfYear? YearEndMonth { get; set; }
    public DateTime? IncorporationDate { get; set; }

    public CompanyType CompanyType { get; set; }
    public string CompanyDirectorName { get; set; } = null!; // First Owner + Director position name
    public int ContactsCount { get; set; }  // Communication Contacts + Official Contacts
    public int MsicCodesCount { get; set; }
    public int WorkCount { get; set; }
    public List<CompanyMsicCodeVM> MsicCodes { get; set; } = new List<CompanyMsicCodeVM>();

}
