using Newtonsoft.Json;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Filters;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SWSA.MvcPortal.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    public string StaffId { get; set; } = null!;
    [SystemAuditLog("Username")]
    public string Username { get; set; } = null!;
    [SystemAuditLog("Full Name")]
    public string FullName { get; set; } = null!;
    [SystemAuditLog("Phone Number")]
    public string PhoneNumber { get; set; } = null!;
    [SystemAuditLog("Email Address")]
    public string Email { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
    [SystemAuditLog("Is Active")]
    public bool IsActive { get; set; } = true;
    [SystemAuditLog("Is Locked")]
    public bool IsLocked { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? LastLoginAt { get; set; }
    [SystemAuditLog("User Role")]
    public UserRole Role { get; set; } = UserRole.Staff;

    //public bool Login(string password)
    //{
    //    if (PasswordHasher.Verify(password, HashedPassword))
    //    {
    //        if (PasswordHasher.NeedsRehash(HashedPassword))
    //        {

    //            HashedPassword = PasswordHasher.Hash(password);
    //        }

    //        LastLoginAt = DateTime.Now;
    //        return true;
    //    }

    //    return false;
    //}

    public virtual ICollection<ScheduledJob> ScheduledJobs { get; set; }
    public virtual ICollection<SystemAuditLog> SystemAuditLogs { get; set; }
    public virtual ICollection<UserCompanyDepartment> CompanyDepartments { get; set; }
    public virtual ICollection<CompanyWorkAssignment> AssignedWorks { get; set; }

    public string ToJsonData()
    {
        return JsonConvert.SerializeObject(this);
    }

    public string GetWhatsappNumber()
    {
        return Regex.Replace(PhoneNumber ?? "", @"\D", "");
    }
}
