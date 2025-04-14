using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum CompanyActivityLevel
{
    /// <summary>
    /// Dormant
    /// </summary>
    [Display(Name = "D")]
    D = 1,

    /// <summary>
    /// Semi-Dormant
    /// </summary>
    [Display(Name = "SD")]
    SD = 2,

    /// <summary>
    /// Active
    /// </summary>
    [Display(Name = "A")]

    A = 3,

    /// <summary>
    /// Active Above Average 
    /// </summary>

    [Display(Name = "AA")]
    AA = 4,

    /// <summary>
    /// Very Active
    /// </summary>

    [Display(Name = "AAA")]
    AAA = 5,

    /// <summary>
    /// Extremely Active 
    /// </summary>
    [Display(Name = "AAAAA")]

    AAAAA = 6
}
