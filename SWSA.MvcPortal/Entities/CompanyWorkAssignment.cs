using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Entities;

public class CompanyWorkAssignment
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public CompanyActivityLevel CompanyActivityLevel { get; set; }
    public WorkType WorkType { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public DateTime DueDate { get; set; }

    [ForeignKey(nameof(AssignedStaff))]
    public int? AssignedStaffId { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime? CompletedDate { get; set; }
    public string? InternalNote { get; set; }
    public virtual Company Company { get; set; } = null!;
    public virtual CompanyStaff? AssignedStaff { get; set; }
    public virtual CompanyWorkProgress Progress { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

}
