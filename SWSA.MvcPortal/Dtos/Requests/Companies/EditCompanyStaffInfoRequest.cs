using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Companies;

public class EditCompanyStaffInfoRequest
{
    public int? CompanyId { get; set; }
    public int? CompanyDepartmentId { get; set; }
    public int ContactId { get; set; }
    public string ContactName { get; set; } = null!;
    public string WhatsApp { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Remark { get; set; } = null!;
    public PositionType Position { get; set; } = PositionType.Staff;
}


public class EditCompanyStaffLoginProfileRequest
{
    public string? Username { get; set; } = null!;
    public string? HashedPassword { get; set; } = null!;
}
