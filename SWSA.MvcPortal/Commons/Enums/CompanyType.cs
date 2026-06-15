using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum CompanyType
{
    [Display(Name = "Sdn Bhd")]
    SdnBhd,
    [Display(Name = "LLP")]
    LLP,
    Partnership,
    [Display(Name = "Sole Proprietorships")]
    SoleProprietorships,
    [Display(Name = "Bhd")]
    Bhd,
    [Display(Name = "Enterprise")]
    Enterprise,
    [Display(Name = "Advocate")]
    Advocate,
    [Display(Name = "Resign")]
    Resign
}
