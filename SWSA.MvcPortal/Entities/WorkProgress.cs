using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Attributes;

namespace SWSA.MvcPortal.Entities;
public class WorkProgress
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(WorkAssignment))]
    public int WorkAssignmentId { get; set; }
    public virtual WorkAssignment WorkAssignment { get; set; } = null!;

    [SystemAuditLog("Work Start Date")]
    public DateTime? StartDate { get; set; }

    [SystemAuditLog("Work End Date")]
    public DateTime? EndDate { get; set; }

    [SystemAuditLog("Time Taken (Days)")]
    public int? TimeTakenInDays { get; set; }

    [SystemAuditLog("Progress Status")]
    public WorkProgressStatus Status { get; set; } = WorkProgressStatus.Pending; // e.g. Pending, InProgress, Completed

    [SystemAuditLog("Progress Note")]
    public string? ProgressNote { get; set; }

    [SystemAuditLog("Is Job Completed")]
    public bool IsJobCompleted { get; set; } = false;

    [SystemAuditLog("Is Backlog Task")]
    public bool IsBacklog { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    public void UpdateProgress()
    {
        if (Status == WorkProgressStatus.Pending)
        {
            Processing();
            if (WorkAssignment.IsComplete())
                Complete();
        }
    }

    private void Complete()
    {
        Status = WorkProgressStatus.Completed;
        IsJobCompleted = true;
        EndDate = DateTime.Today;
        UpdatedAt = DateTime.Now;
    }

    private void Processing()
    {
        Status = WorkProgressStatus.InProgress;
        if (!StartDate.HasValue)
            StartDate = DateTime.Today;
        UpdatedAt = DateTime.Now;
    }
}