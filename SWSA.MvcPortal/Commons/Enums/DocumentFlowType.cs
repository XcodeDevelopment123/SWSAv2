using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum DocumentFlowType
{
    [Display(Name = "Unset")]
    Unset = 0,
    [Display(Name = "Incoming")]
    Incoming = 1,
    [Display(Name = "Outgoing")]
    Outgoing = 2,
}
