using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Users;

public class EditUserRequest
{
    public string StaffId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; }  = null!;
    public string PhoneNumber { get; set; }  = null!;
    public string? Password { get; set; }  = null!;
    public bool IsActive { get; set; }
    public UserRole Role { get; set; }  
}
