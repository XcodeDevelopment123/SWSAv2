
namespace SWSA.MvcPortal.Dtos.Requests.Companies;

public class CreateCompanyComplianceDate
{
    public DateTime? FirstYearAccountStart { get; set; } // 18-month rule
    public DateTime? AGMDate { get; set; }
    public DateTime? AccountDueDate { get; set; }
    public DateTime? AnniversaryDate { get; set; }
    public DateTime? AnnualReturnDueDate { get; set; }
    public string? Notes { get; set; }

}
