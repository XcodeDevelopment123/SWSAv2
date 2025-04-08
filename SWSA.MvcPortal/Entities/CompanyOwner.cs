using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;

public class CompanyOwner
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public string NamePerIC { get; set; } = null!;
    public string ICOrPassportNumber { get; set; } = null!;
    public PositionType Position { get; set; } = PositionType.Director;
    public string TaxReferenceNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public OwnershipType OwnershipType { get; set; } //Its own or corp with other

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
