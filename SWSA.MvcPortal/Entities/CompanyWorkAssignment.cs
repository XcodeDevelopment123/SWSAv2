using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Filters;

namespace SWSA.MvcPortal.Entities;

public class CompanyWorkAssignment
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; } = null!;
    [SystemAuditLog("Work Type")]
    public WorkType WorkType { get; set; } 
    [SystemAuditLog("Company Activity Level")]
    public CompanyActivityLevel CompanyActivityLevel { get; set; } // SSM-compliant business size

    [SystemAuditLog("Service Scope")]
    public ServiceScope ServiceScope { get; set; }

    [SystemAuditLog("Internal Note")]
    public string? InternalNote { get; set; }

    [SystemAuditLog("Company Status for This Task")]
    public CompanyStatus CompanyStatus { get; set; } // Dormant, Active, etc

    [SystemAuditLog("Is Year-End Related Task")]
    public bool IsYearEndTask { get; set; } // YE to do
    public virtual CompanyWorkProgress? Progress { get; set; }
    public virtual ICollection<DocumentRecord> Documents { get; set; } = new List<DocumentRecord>();
    public virtual ICollection<WorkAssignmentUserMapping> AssignedUsers { get; set; } = new List<WorkAssignmentUserMapping>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    #region Work Type Entity (Only one)
    public virtual AnnualReturnSubmission? ARSubmission { get; set; }
    public virtual CompanyStrikeOffSubmission? StrikeOffSubmission { get; set; } = null!;

    public void CreateSubmissionEntity()
    {
        switch (WorkType)
        {
            case WorkType.AnnualReturn:
                ARSubmission = new AnnualReturnSubmission();
                break;
            case WorkType.StrikeOff:
                StrikeOffSubmission = new CompanyStrikeOffSubmission();
                break;
            // Add other cases as needed
            default:
                throw new NotSupportedException($"WorkType {WorkType} is not supported for submission creation.");
        }
    }
    #endregion
}
