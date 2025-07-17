using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Contacts;

public class UpsertCompanyOwnerRequest
{
    public int? Id { get; set; } //entity id, if have value is update
    public int ClientId { get; set; }
    public string Name { get; set; } = null!;
    public string ICOrPassport { get; set; } = null!;
    public PositionType Position { get; set; } = PositionType.Director;
    public string TaxRef { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public bool IsRequireSubmitFormBE { get; set; } = false;
}
