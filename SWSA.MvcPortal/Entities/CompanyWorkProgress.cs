using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Entities;

public class CompanyWorkProgress
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(WorkAssignment))]
    public int WorkAssignmentId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? TimeTakenInDays { get; set; }
    public WorkProgressStatus Status { get; set; } = WorkProgressStatus.Pending;
    public string? ProgressNote { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public virtual CompanyWorkAssignment WorkAssignment { get; set; } = null!;
}
