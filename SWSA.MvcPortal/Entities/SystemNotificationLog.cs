using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Services.Messaging.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

[Module("SystemInfra")]
public class SystemNotificationLog
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [StringLength(255)]
    public string Recipient { get; set; } = null!;
    [StringLength(128)]
    public string TemplateCode { get; set; } = null!;
    public MessagingChannel Channel { get; set; }
    public bool IsSuccess { get; set; }
    [StringLength(255)]
    public string ResultMessage { get; set; } = null!;
    [StringLength(255)]
    public string? Reason { get; set; }
}
