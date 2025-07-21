using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.Clients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SWSA.MvcPortal.Entities;

public class CommunicationContact
{
    [Key]
    public int Id { get; set; }
    [SystemAuditLog("Contact Name")]
    public string ContactName { get; set; } = null!;
    [SystemAuditLog("WhatsApp Number")]
    public string WhatsApp { get; set; } = null!;
    [SystemAuditLog("Email Address")]
    public string Email { get; set; } = null!;
    [SystemAuditLog("Remarks")]
    public string? Remark { get; set; }
    [SystemAuditLog("Position")]
    public PositionType Position { get; set; } = PositionType.Staff;
    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }
    public virtual BaseClient Client { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string GetWhatsappNumber()
    {
        return Regex.Replace(this.WhatsApp ?? "", @"\D", "");
    }
}
