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

    [SystemAuditLog("SSM Extension Date")]
    public DateTime? SSMExtensionDate { get; set; }

    [SystemAuditLog("Annual General Meeting Date")]
    public DateTime? AGMDate { get; set; } //Annual General Meeting

    [SystemAuditLog("Annual Return Due Date")]
    public DateTime? ARDueDate { get; set; } //Annual Return Due Date

    [SystemAuditLog("Reminder Date")]
    public DateTime? ReminderDate { get; set; }

    public virtual CompanyWorkProgress? Progress { get; set; }
    public virtual AnnualReturnSubmission? Submission { get; set; }
    public virtual ICollection<WorkAssignmentUserMapping> AssignedUsers { get; set; } = new List<WorkAssignmentUserMapping>();
    public virtual ICollection<WorkAssignmentMonth> PlannedMonths { get; set; } = new List<WorkAssignmentMonth>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    #region Audit Log Use
    [NotMapped]
    [SystemAuditLog("Month Accounting Is Planned")]
    public string AccountPlannedMonths_AuditLabel { get; set; } = default!;
    [NotMapped]
    [SystemAuditLog("Month Is Planned")]
    public string PlannedMonths_AuditLabel { get; set; } = default!;

    public void GenerateAuditLabel()
    {
        PlannedMonths_AuditLabel = string.Join(", ", PlannedMonths.Select(x => x.Month.ToString()));
    }
    #endregion
}
