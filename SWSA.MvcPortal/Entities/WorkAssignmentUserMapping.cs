using SWSA.MvcPortal.Commons.Filters;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

public class WorkAssignmentUserMapping
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(WorkAssignment))]
    public int WorkAssignmentId { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [SystemAuditLog("Assigned Department")]
    public string Department { get; set; } = null!; // e.g. "Audit", "Tax", "Secretary"

    public virtual CompanyWorkAssignment WorkAssignment { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
