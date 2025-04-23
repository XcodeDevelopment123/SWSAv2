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
    [SystemAuditLog("Company Activity Level")]
    public CompanyActivityLevel CompanyActivityLevel { get; set; }
    [SystemAuditLog("Work Type")]
    public WorkType WorkType { get; set; }
    [SystemAuditLog("Service Scope")]
    public ServiceScope ServiceScope { get; set; }
    [SystemAuditLog("Due Date")]
    public DateTime DueDate { get; set; }

    [ForeignKey(nameof(AssignedStaff))]
    public int? AssignedStaffId { get; set; }

    [SystemAuditLog("Is Completed")]
    public bool IsCompleted { get; set; } = false;
    [SystemAuditLog("Completed Date")]
    public DateTime? CompletedDate { get; set; }
    public string? InternalNote { get; set; }
    public virtual Company Company { get; set; } = null!;
    public virtual CompanyStaff? AssignedStaff { get; set; }
    public virtual CompanyWorkProgress Progress { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
