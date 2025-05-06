using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Filters;

namespace SWSA.MvcPortal.Commons.Enums;

public enum UserRole
{
    [EnumIgnore]
    Unknown = 0,
    [Display(Name = "Super Admin")]
    SuperAdmin = 100,
    Staff = 200
}
