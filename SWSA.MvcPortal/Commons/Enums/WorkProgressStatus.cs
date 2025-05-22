using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Filters;

namespace SWSA.MvcPortal.Commons.Enums;

public enum WorkProgressStatus
{
    [EnumIgnore]
    Unknown = 0,
    Pending = 1,
    [Display(Name = "In Progress")] 
    InProgress,
    Completed,
    Backlog
}
