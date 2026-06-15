using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum CompanyStatus
{
    [Display(Name = "Active")]
    Active,
    [Display(Name = "Dormant")]
    Dormant,
    [Display(Name = "Strike-off")]
    StrikeOff,
    [Display(Name = "Liquidation")]
    Liquidation,
    [Display(Name = "Resign")]
    Resign
}
