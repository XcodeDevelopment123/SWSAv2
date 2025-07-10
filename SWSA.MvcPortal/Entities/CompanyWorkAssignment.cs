using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Attributes;

namespace SWSA.MvcPortal.Entities;

[Module("WorkAssignment")]
public abstract class CompanyWorkAssignment
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; } = null!;
    public int ForYear { get; set; }

    [SystemAuditLog("Work Type")]
    public WorkType WorkType { get; set; }

    [SystemAuditLog("Service Scope")]
    public ServiceScope ServiceScope { get; set; }

    [SystemAuditLog("Internal Note")]
    public string? InternalNote { get; set; }

    [SystemAuditLog("Company Status")]   //The status when work create
    public CompanyStatus CompanyStatus { get; set; }

    [SystemAuditLog("Company Activity Level")]//The level when work create
    public CompanyActivityLevel CompanyActivityLevel { get; set; }

    [SystemAuditLog("Reminder Date")]
    public DateTime? ReminderDate { get; set; }
    public virtual CompanyWorkProgress? Progress { get; set; }
    public virtual ICollection<WorkAssignmentUserMapping> AssignedUsers { get; set; } = new List<WorkAssignmentUserMapping>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    public abstract bool IsComplete();

    public string GetForYearLabel()
    {
        return $"{ForYear} - {ForYear + 1}";
    }
}
