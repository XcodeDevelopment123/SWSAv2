using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum CreditRating
{
    [Display(Name = "Excellent")]
    Excellent,
    [Display(Name = "Good")]
    Good,
    [Display(Name = "Fair")]
    Fair,
    [Display(Name = "Poor")]
    Poor
}
