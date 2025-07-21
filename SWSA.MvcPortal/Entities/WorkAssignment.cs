using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.Clients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;

public abstract class WorkAssignment
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }
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
    public virtual WorkProgress? Progress { get; set; }
    public virtual ICollection<WorkAssignmentUserMapping> AssignedUsers { get; set; } = new List<WorkAssignmentUserMapping>();
    public virtual BaseClient Client { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    public abstract bool IsComplete();

    public string GetForYearLabel()
    {
        return $"{ForYear} - {ForYear + 1}";
    }
}
