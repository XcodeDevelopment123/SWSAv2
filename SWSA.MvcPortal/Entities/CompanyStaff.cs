using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SWSA.MvcPortal.Entities;

public class CompanyStaff
{
    [Key]
    public int Id { get; set; }
    public string StaffId { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string WhatsApp { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Remark { get; set; }
    public PositionType Position { get; set; } = PositionType.Staff;

    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    // Optional (Login Profile)
    public string? Username { get; set; } = null!;
    public string? HashedPassword { get; set; } = null!;
    public bool IsLoginEnabled { get; set; } = false;

    [ForeignKey(nameof(CompanyDepartmentId))]
    public int? CompanyDepartmentId { get; set; }

    public virtual Company Company { get; set; } = null!;
    public virtual CompanyDepartment? CompanyDepartment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? LastLoginAt { get; set; }

    public string GetWhatsappNumber()
    {
        return Regex.Replace(this.WhatsApp ?? "", @"\D", "");
    }
}
