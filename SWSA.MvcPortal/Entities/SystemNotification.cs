using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;

public class SystemNotification
{
    public int Id { get; set; }
    public NotificationType Type { get; set; } 
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ReadAt { get; set; }

    [ForeignKey(nameof(CompanyStaff))]
    public int CompanyStaffId { get; set; }
    public CompanyStaff CompanyStaff { get; set; } = null!;
    public int? WorkAssignmentId { get; set; } 
    public virtual CompanyWorkAssignment? WorkAssignment { get; set; }
}
