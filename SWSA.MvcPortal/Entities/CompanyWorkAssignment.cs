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
    public WorkType WorkType { get; set; } // e.g. Annual Return, Audit Filing

    [SystemAuditLog("Company Activity Level")]
    public CompanyActivityLevel CompanyActivityLevel { get; set; } // SSM-compliant business size

    [SystemAuditLog("Service Scope")]
    public ServiceScope ServiceScope { get; set; }

    [SystemAuditLog("Due Date")]
    public DateTime DueDate { get; set; } // Required submission deadline

    [SystemAuditLog("Is Completed")]
    public bool IsCompleted { get; set; } = false;

    [SystemAuditLog("Completed Date")]
    public DateTime? CompletedDate { get; set; }

    [SystemAuditLog("Internal Note")]
    public string? InternalNote { get; set; }

    [SystemAuditLog("Company Status for This Task")]
    public CompanyStatus CompanyStatus { get; set; } // Dormant, Active, etc

    [SystemAuditLog("Company Activity Code")]
    public string? CompanyActivityType { get; set; } // A, AA, SD, etc

    [SystemAuditLog("Is Year-End Related Task")]
    public bool IsYearEndActionRequired { get; set; } // YE to do

    [SystemAuditLog("Month Audit Is Planned")]
    public int? AuditMonthToDo { get; set; } // Month (1–12)

    [ForeignKey(nameof(AssignedUser))]
    public int? AssignedUserId { get; set; }
    public virtual User? AssignedUser { get; set; }

    [ForeignKey(nameof(AssignedAuditUser))]
    public int? AssignedAuditUserId { get; set; }

    [SystemAuditLog("Audit Staff Assigned")]
    public virtual User? AssignedAuditUser { get; set; }

    [SystemAuditLog("AGM Date")]
    public DateTime? AGMDate { get; set; }

    [SystemAuditLog("Annual Return Due Date")]
    public DateTime? ARDueDate { get; set; }

    [SystemAuditLog("Reminder Date")]
    public DateTime? ReminderDate { get; set; }

    public virtual CompanyWorkProgress Progress { get; set; } = null!;
    public virtual ICollection<WorkAssignmentUserMapping> AssignedUsers { get; set; } = new List<WorkAssignmentUserMapping>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
