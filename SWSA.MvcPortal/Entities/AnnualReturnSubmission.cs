using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Filters;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;

public class AnnualReturnSubmission
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; } = null!;

    [SystemAuditLog("Submission Year")]
    public int Year { get; set; } // e.g. 2025

    [SystemAuditLog("Anniversary Date")]
    public DateTime? AnniversaryDate { get; set; }

    [SystemAuditLog("AR Due Date")]
    public DateTime? ARDueDate { get; set; }

    [SystemAuditLog("Targeted AR Date")]
    public DateTime? TargetedARDate { get; set; }

    [SystemAuditLog("Actual Date of Annual Return")]
    public DateTime? DateOfAnnualReturn { get; set; }

    [SystemAuditLog("Date AR Submitted")]
    public DateTime? DateSubmitted { get; set; }

    [SystemAuditLog("Date Sent to Client")]
    public DateTime? DateSentToClient { get; set; }

    [SystemAuditLog("Date Returned by Client")]
    public DateTime? DateReturnedByClient { get; set; }

    [SystemAuditLog("Remarks")]
    public string? Remarks { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}