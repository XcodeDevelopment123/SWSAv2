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

    public virtual CompanyWorkAssignment WorkAssignment { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
