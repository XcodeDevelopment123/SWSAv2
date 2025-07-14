using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Entities.Clients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;

[Module("CompanyProfile")]
public class OfficialContact
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

    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }
    public virtual BaseClient Client { get; set; } = null!;
}
