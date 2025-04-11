using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Dtos.Requests.Companies;

namespace SWSA.MvcPortal.Entities;

public class CompanyComplianceDate
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public DateTime? FirstYearAccountStart { get; set; } // 18-month rule
    public DateTime? AGMDate { get; set; }
    public DateTime? AccountDueDate { get; set; }
    public DateTime? AnniversaryDate { get; set; }
    public DateTime? AnnualReturnDueDate { get; set; }
    public string? Notes { get; set; }
    public Company Company { get; set; } = null!;


    public void UpdateComplianceDates(EditCompanyComplianceDate req)
    {
        AccountDueDate = req.AccountDueDate;
        AnniversaryDate = req.AnniversaryDate;
        AGMDate = req.AGMDate;
        AnnualReturnDueDate = req.AnnualReturnDueDate;
        FirstYearAccountStart = req.FirstYearAccountStart;
        Notes = req.Notes;
    }
}
