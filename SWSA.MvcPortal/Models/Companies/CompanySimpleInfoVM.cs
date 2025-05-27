using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.Companies;

public class CompanySimpleInfoVM
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string RegistrationNumber { get; set; }
    public CompanyType CompanyType { get; set; }
    public MonthOfYear? YearEndMonth { get; set; }
    public DateTime? IncorporationDate { get; set; }
}
