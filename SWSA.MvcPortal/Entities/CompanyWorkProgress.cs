using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Filters;

namespace SWSA.MvcPortal.Entities;

public class CompanyWorkProgress
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(WorkAssignment))]
    public int WorkAssignmentId { get; set; }
    [SystemAuditLog("Work Start Date")]
    public DateTime? StartDate { get; set; }
    [SystemAuditLog("Work End Date")]
    public DateTime? EndDate { get; set; }
    [SystemAuditLog("Time Taken (Days)")]
    public int? TimeTakenInDays { get; set; }
    [SystemAuditLog("Progress Status")]
    public WorkProgressStatus Status { get; set; } = WorkProgressStatus.Pending;
    [SystemAuditLog("Progress Note")]
    public string? ProgressNote { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public virtual CompanyWorkAssignment WorkAssignment { get; set; } = null!;
}
