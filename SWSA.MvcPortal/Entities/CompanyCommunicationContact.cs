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
    public string ContactName { get; set; } = null!;
    public string WhatsApp { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Remark { get; set; }
    public PositionType Position { get; set; } = PositionType.Staff;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Optional 
    [ForeignKey(nameof(CompanyDepartmentId))]
    public int? CompanyDepartmentId { get; set; }

    public Company Company { get; set; } = null!;
    public CompanyDepartment? Department { get; set; }
}
