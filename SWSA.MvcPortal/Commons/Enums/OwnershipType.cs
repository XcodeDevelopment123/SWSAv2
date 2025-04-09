using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum OwnershipType
{
    [Display(Name = "Individual Owner")]
    IndividualOwner,

    [Display(Name = "Corporate Owner")]
    CorporateOwner,

    [Display(Name = "Majority Shareholder")]
    Majority,

    [Display(Name = "Minority Shareholder")]
    Minority,

    [Display(Name = "Equal Partner")]
    Equal,

    [Display(Name = "Foreign Owner")]
    Foreign,

    [Display(Name = "Local Owner")]
    Local,

    [Display(Name = "Nominee Owner")]
    Nominee,

    [Display(Name = "Sole Owner")]
    Sole,

    [Display(Name = "Joint Owner")]
    Joint
}