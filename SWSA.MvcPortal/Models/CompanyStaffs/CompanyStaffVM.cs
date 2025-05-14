using Newtonsoft.Json;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.CompanyStaffs;

public class CompanyStaffVM
{
    public string StaffId { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string WhatsApp { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Remark { get; set; }
    public PositionType Position { get; set; } = PositionType.Staff;
    public Company Company { get; set; } = null!;
    public int CompanyId { get; set; }
    public int? CompanyDepartmentId { get; set; }
    public CompanyDepartment CompanyDepartment { get; set; } = null!;

    public string ToJsonData()
    {
        return JsonConvert.SerializeObject(this);
    }
}
