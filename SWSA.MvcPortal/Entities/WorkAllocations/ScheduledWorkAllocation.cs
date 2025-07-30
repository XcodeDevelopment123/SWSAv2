using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Entities.Reminders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities.WorkAllocations;

//The actual work allocation for the year
public class ScheduledWorkAllocation
{
    [Key]
    public int Id { get; set; }
    public int ForYear { get; set; }
    public ServiceScope ServiceScope { get; set; } 
    public string? Remarks { get; set; }
    public int ClientId { get; set; }
    public DateTime TargetedToStart { get; set; }
    public WorkPriority Priority { get; set; } = WorkPriority.None;
    //PIC
    public int AssignedUserId { get; set; }

    [ForeignKey(nameof(AssignedUserId))]
    public User AssignedUser { get; set; }

    [ForeignKey(nameof(ClientId))]
    public BaseClient Client { get; set; }

    public ICollection<Reminder> Reminders { get; set; } = [];
}
