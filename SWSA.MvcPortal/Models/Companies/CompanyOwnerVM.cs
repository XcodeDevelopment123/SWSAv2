using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Models.Companies;

public class CompanyOwnerVM
{
    [Key]
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string NamePerIC { get; set; } = null!;
    public string ICOrPassportNumber { get; set; } = null!;
    public PositionType Position { get; set; } = PositionType.Director;
    public string TaxReferenceNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public OwnershipType OwnershipType { get; set; } //Its own or corp with other
}
