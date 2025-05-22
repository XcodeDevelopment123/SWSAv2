using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Filters;
using SWSA.MvcPortal.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Models.Companies;

public class CompanyOwnerVM
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
}
