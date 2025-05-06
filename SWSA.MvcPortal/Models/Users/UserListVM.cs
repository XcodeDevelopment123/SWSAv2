using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.Users;

public class UserListVM
{
    public string StaffId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public bool IsLocked { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public UserRole Role { get; set; }
}
