using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum CompanyStatus
{
    Active = 1,
    Dormant = 2,
    [Display(Name = "Strike Off")]
    StrikeOff = 3,
    [Display(Name = "Under Review")]
    UnderReview = 4,
    Draft = 5
}
