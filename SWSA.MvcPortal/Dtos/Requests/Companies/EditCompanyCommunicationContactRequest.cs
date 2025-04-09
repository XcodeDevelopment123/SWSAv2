using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Companies;

public class EditCompanyCommunicationContactRequest
{
    public int? CompanyId { get; set; }
    public int ContactId { get; set; }
    public string ContactName { get; set; } = null!;
    public string WhatsApp { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Remark { get; set; } = null!;
    public PositionType Position { get; set; } = PositionType.Staff;
}
