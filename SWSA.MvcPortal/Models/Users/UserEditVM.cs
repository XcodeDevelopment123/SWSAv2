using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.Users;

public class UserEditVM
{
    public string StaffId { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public UserRole Role { get; set; }
}
