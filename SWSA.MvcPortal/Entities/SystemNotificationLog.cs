using SWSA.MvcPortal.Commons.Services.Messaging.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

public class SystemNotificationLog
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [StringLength(255)]
    public string Recipient { get; set; } = null!;
    public MessagingChannel Channel { get; set; }
    public bool IsSuccess { get; set; }
    [StringLength(255)]
    public string ResultMessage { get; set; } = null!;
}
