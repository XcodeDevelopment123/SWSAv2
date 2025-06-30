using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Attributes;

namespace SWSA.MvcPortal.Entities.ValueOfObject;

public abstract class BaseSubmission
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(WorkAssignment))]
    public int WorkAssignmentId { get; set; }
    public virtual CompanyWorkAssignment WorkAssignment { get; set; } = null!;
    [SystemAuditLog("Remarks")]
    public string? Remarks { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    public int ForYear { get; set; } 

    public abstract bool IsSubmissionComplete();
}
