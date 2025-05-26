using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum DocumentType
{
    Other = 0,
    [Display(Name = "SSM")]
    SSM,
    [Display(Name = "LHDN")]
    LHDN,
    [Display(Name = "EPF")]
    EPF,
    [Display(Name = "SOCSO")]
    SOCSO,
}

