using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Filters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SWSA.MvcPortal.Entities;

public class CompanyStaff
{
    [Key]
    public int Id { get; set; }
    public string StaffId { get; set; } = null!;
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
    public virtual ICollection<SystemAuditLog> SystemAuditLogs { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? LastLoginAt { get; set; }

    public string GetWhatsappNumber()
    {
        return Regex.Replace(this.WhatsApp ?? "", @"\D", "");
    }
}
