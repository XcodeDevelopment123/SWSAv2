using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Filters;
using SWSA.MvcPortal.Dtos.Requests.Companies;

namespace SWSA.MvcPortal.Entities;

public class CompanyComplianceDate
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }

    [SystemAuditLog("First Year Account Start Date")]
    public DateTime? FirstYearAccountStart { get; set; } // 18-month rule
    [SystemAuditLog("AGM Date")]
    public DateTime? AGMDate { get; set; }
    [SystemAuditLog("Account Due Date")]
    public DateTime? AccountDueDate { get; set; }
    [SystemAuditLog("Anniversary Date")]
    public DateTime? AnniversaryDate { get; set; }
    [SystemAuditLog("Annual Return Due Date")]
    public DateTime? AnnualReturnDueDate { get; set; }
    [SystemAuditLog("Compliance Notes")]
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
