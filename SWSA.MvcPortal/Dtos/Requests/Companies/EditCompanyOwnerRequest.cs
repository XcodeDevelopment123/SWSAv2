using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Companies;

public class EditCompanyOwnerRequest
{
    public int OwnerId { get; set; }    
    public int CompanyId { get; set; }
    public string NamePerIC { get; set; } = null!;
    public string ICOrPassportNumber { get; set; } = null!;
    public PositionType Position { get; set; } //PositionType
    public string TaxReferenceNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public OwnershipType OwnershipType { get; set; }
}
