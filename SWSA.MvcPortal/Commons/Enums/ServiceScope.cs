using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum ServiceScope
{
    [Display(Name = "Full Set Accounting")]
    FullSetAccounting,
    [Display(Name = "Partial Set Accounting")]
    AccountSingleEntry,
    Tax,
    Review,
    [Display(Name = "E Invoice")]
    E_Invoice,
    Retainer,
    Software,
    Other=99,
}
