using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Filters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SWSA.MvcPortal.Entities;

public class CompanyCommunicationContact
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
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string GetWhatsappNumber()
    {
        return Regex.Replace(this.WhatsApp ?? "", @"\D", "");
    }
}
