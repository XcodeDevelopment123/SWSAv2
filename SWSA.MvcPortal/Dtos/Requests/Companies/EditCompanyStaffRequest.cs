using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Companies;

public class EditCompanyStaffInfoRequest
{
    public string StaffId { get; set; }
    public int? CompanyId { get; set; }
    public int? CompanyDepartmentId { get; set; }
    public string ContactName { get; set; } = null!;
    public string WhatsApp { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Remark { get; set; } = null!;
    public PositionType Position { get; set; } = PositionType.Staff;
}