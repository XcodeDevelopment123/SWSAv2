using System.ComponentModel.DataAnnotations.Schema;
using SWSA.MvcPortal.Commons.Filters;
using SWSA.MvcPortal.Entities.ValueOfObject;

namespace SWSA.MvcPortal.Entities;

public class CompanyStrikeOffSubmission : BaseSubmission
{
    [ForeignKey(nameof(Company))]
    [SystemAuditLog("Company ID")]
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; } = null!;

    [SystemAuditLog("Strike-Off Start Date")]
    public DateTime? StartDate { get; set; }

    [SystemAuditLog("Strike-Off Complete Date")]
    public DateTime? CompleteDate { get; set; }

    [SystemAuditLog("SSM Submission Date")]
    public DateTime? SSMSubmissionDate { get; set; }

    [SystemAuditLog("IRB Submission Date")]
    public DateTime? IRBSubmissionDate { get; set; }

    [SystemAuditLog("SSM Strike-Off Date")]
    public DateTime? SSMStrikeOffDate { get; set; }
}
