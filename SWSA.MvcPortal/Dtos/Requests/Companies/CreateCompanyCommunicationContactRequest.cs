using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Companies;

public class CreateCompanyCommunicationContactRequest
{
    public int? CompanyId { get; set; }
    public string ContactName { get; set; } = null!;
    public string WhatsApp { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Remark { get; set; } = null!;
    public int? CompanyDepartmentId { get; set; } = null!;
    public PositionType Position { get; set; } = PositionType.Staff;
}
