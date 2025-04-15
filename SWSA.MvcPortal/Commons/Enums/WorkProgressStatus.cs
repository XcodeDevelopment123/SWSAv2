using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum WorkProgressStatus
{
    Unknown = 0,
    Pending = 1,
    [Display(Name = "In Progress")] 
    InProgress,
    Completed,
    Backlog
}
