using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Entities.Clients;

namespace SWSA.MvcPortal.Entities;

//Just for references
[Module("Companies")]
public class CompanyComplianceDate
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }

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

    public BaseCompany Client { get; set; }

}
