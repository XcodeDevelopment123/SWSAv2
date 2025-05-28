using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;
public enum WorkType
{
    Audit,
    [Display(Name = "Company Strike Off")]
    StrikeOff,
    Accounting,
    [Display(Name = "Sdn Bhd")]
    SdnBhd,
    [Display(Name = "LLP")]
    LLP,
    Enterprise,
    Partnership,
    [Display(Name = "Form BE")]
    FormBE,
    [Display(Name = "Form B")]
    FormB,
    Trust,
    [Display(Name = "Annual Return")]
    AnnualReturn,
}