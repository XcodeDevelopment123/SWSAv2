using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Users;

public class CreateUserRequest
{
    public string Username { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsActive { get; set; }
    public UserRole Role { get; set; }
    public string Title { get; set; }
    public string Department { get; set; }
    public DateTime JoinDate { get; set; }
}
