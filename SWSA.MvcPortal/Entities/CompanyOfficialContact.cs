using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

public class CompanyOfficialContact
{
    [Key]
    public int Id { get; set; }
    public string Address { get; set; } = null!;
    public string OfficeTel { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Remark { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
