using Newtonsoft.Json;
using SWSA.MvcPortal.Commons.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    public string StaffId { get; set; } = null!;

    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public bool IsLocked { get; set; } = false;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    //  public UserRole Role { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? LastLoginAt { get; set; }


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

    public string ToJsonData()
    {
        return JsonConvert.SerializeObject(this);
    }
}
