using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using SWSA.MvcPortal.Commons.Helpers;

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
    [SystemAuditLog("Join Date")]
    public DateTime JoinDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? LastLoginAt { get; set; }
    [SystemAuditLog("Title")]
    public string Title { get; set; } = null!;
    [SystemAuditLog("Department")]
    public string Department { get; set; }
    [SystemAuditLog("User Role")]
    public UserRole Role { get; set; } = UserRole.Staff;

    public virtual ICollection<ScheduledJob> ScheduledJobs { get; set; }
    public virtual ICollection<SystemAuditLog> SystemAuditLogs { get; set; }
    public virtual ICollection<WorkAssignmentUserMapping> AssignedWorks { get; set; } = new List<WorkAssignmentUserMapping>(); // The user handle work


    public void UpdateInfo(string staffId, string fullName, string phoneNumber, string email)
    {
        StaffId = staffId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public void UpdateDepartmentAndTitle(string department, string title)
    {
        Title = title;
        Department = department;
    }

    public void UpdateRole(UserRole role)
    {
        Role = role;
    }

    public void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }


    public string GetWhatsappNumber()
    {
        return Regex.Replace(PhoneNumber ?? "", @"\D", "");
    }

    public void SetAndHashPassword(string password)
    {
        HashedPassword = PasswordHasher.Hash(password);
    }
}
