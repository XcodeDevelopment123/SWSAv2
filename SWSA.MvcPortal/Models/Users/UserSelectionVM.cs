using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.Users;

public class UserSelectionVM
{
    public string StaffId { get; set; }
    public string FullName { get; set; }
    public UserRole Role { get; set; }
}
