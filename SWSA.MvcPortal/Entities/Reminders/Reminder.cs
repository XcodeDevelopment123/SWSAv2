using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities.Reminders;

public class Reminder // Reminder Template, each entity is a remind action
{
    [Key]
    public int Id { get; set; }
    
    [SystemAuditLog("Title")]
    public string Title { get; set; } = null!;
    
    [SystemAuditLog("Target Date")]
    public DateTime TargetAt { get; set; }
    
    [SystemAuditLog("Reminder Type")]
    public ReminderType Type { get; set; }
    
    [SystemAuditLog("Status")]
    public ReminderStatus Status { get; set; }
  
    public int ScheduledWorkAllocationId { get; set; }
    
    [SystemAuditLog("Notification Channel")]
    public ReminderChannel Channel { get; set; }
    
    [SystemAuditLog("Is Sent")]
    public bool IsSent { get; set; }
    
    public DateTime? SentAt { get; set; }
    public string? SentToEmail { get; set; }
    public string? SentToPhone { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
