
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkUserVM
{
    public string StaffId { get; set; } = null!;
    public string StaffName { get; set; } = null!;
    public UserRole Role { get; set;}
}
