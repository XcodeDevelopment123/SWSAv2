using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;

public class CompanyCommunicationContact
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public PositionType Position { get; set; } = PositionType.Staff;
    public string WhatsApp { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? Remark { get; set; }
}
