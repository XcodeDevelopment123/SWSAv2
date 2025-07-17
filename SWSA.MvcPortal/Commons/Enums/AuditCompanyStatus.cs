using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum AuditCompanyStatus
{
    [Display(Name ="Small AEX")]
    SmallAEX = 0,
    Active = 10,
    Resigned = 20,
}
