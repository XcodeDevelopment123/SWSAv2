using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum ScheduleType
{
    Once,
    Daily,
    Weekly,
    Monthly,
    [Display(Name ="Custom Cron")]
    Cron
}
