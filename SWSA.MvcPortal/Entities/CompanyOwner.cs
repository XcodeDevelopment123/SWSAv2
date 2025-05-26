using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;

[Module("CompanyProfile")]
public class CompanyOwner
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    [SystemAuditLog("Owner Name (per IC)")]
    public string NamePerIC { get; set; } = null!;
    [SystemAuditLog("IC or Passport Number")]
    public string ICOrPassportNumber { get; set; } = null!;
    [SystemAuditLog("Position")]
    public PositionType Position { get; set; } = PositionType.Director;
    [SystemAuditLog("Tax Reference Number")]
    public string TaxReferenceNumber { get; set; } = null!;
    [SystemAuditLog("Email Address")]
    public string Email { get; set; } = null!;
    [SystemAuditLog("Phone Number")]
    public string PhoneNumber { get; set; } = null!;
    [SystemAuditLog("Ownership Type")]
    public OwnershipType OwnershipType { get; set; } //Its own or corp with other

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Company Company { get; set; } = null!;
}
