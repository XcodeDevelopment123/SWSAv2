using Newtonsoft.Json;

namespace SWSA.MvcPortal.Models.Users;

public class UserVM
{
    public string StaffId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastLoginAt { get; set; }

    public string ToJsonData()
    {
        return JsonConvert.SerializeObject(this);
    }
}
