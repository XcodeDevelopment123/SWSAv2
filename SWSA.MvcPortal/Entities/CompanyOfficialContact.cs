using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Attributes;

namespace SWSA.MvcPortal.Entities;

[Module("CompanyProfile")]
public class CompanyOfficialContact
{
    [Key]
    public int Id { get; set; }

    [SystemAuditLog("Official Address")]
    public string Address { get; set; } = null!;

    [SystemAuditLog("Office Telephone")]
    public string OfficeTel { get; set; } = null!;

    [SystemAuditLog("Official Email")]
    public string Email { get; set; } = null!;

    [SystemAuditLog("Remarks")]
    public string? Remark { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
