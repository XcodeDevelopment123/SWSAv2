using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum OwnershipType
{
    [Display(Name = "Individual Owner")]
    IndividualOwner,

    [Display(Name = "Corporate Owner")]
    CorporateOwner,

    [Display(Name = "Majority Shareholder")]
    MajorityShareholder,

    [Display(Name = "Minority Shareholder")]
    MinorityShareholder,

    [Display(Name = "Equal Partner")]
    EqualPartner,

    [Display(Name = "Foreign Owner")]
    ForeignOwner,

    [Display(Name = "Local Owner")]
    LocalOwner,

    [Display(Name = "Nominee Owner")]
    NomineeOwner,

    [Display(Name = "Sole Owner")]
    SoleOwner,

    [Display(Name = "Joint Owner")]
    JointOwner
}