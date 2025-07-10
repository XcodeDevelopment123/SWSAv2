using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum CompanyStatus
{
    Other=0,
    Active = 1,
    Dormant = 2,
    [Display(Name = "Strike Off")]
    StrikedOff = 3,
    [Display(Name = "Under Review")]
    UnderReview = 4,
    Draft = 5
}
