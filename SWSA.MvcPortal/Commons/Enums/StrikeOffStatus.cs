using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum StrikeOffStatus
{
    [Display(Name = "Not Applied")]
    NotApplied = 0,
    Applying = 1,
    Completed = 2,
}