using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum NotificationType
{
    /// <summary>
    /// 提醒：任务即将到期（如 CP204 Filing）
    /// </summary>

    [Display(Name = "Assignment Due Soon")]
    AssignmentDueSoon = 1,

    /// <summary>
    /// 提醒：任务已逾期仍未完成
    /// </summary>
    [Display(Name = "Assignment Overdue")]
    AssignmentOverdue = 2,

    /// <summary>
    /// 提醒：你被指派了新任务
    /// </summary>

    [Display(Name = "Progress Update")]
    AssignmentAssigned = 3,

    /// <summary>
    /// 提醒：客户上传了新文件，尚未处理
    /// </summary>
    [Display(Name = "Progress Pending")]
    DocumentPending = 4,

    /// <summary>
    /// 提醒：你上传的文件已被处理
    /// </summary>
    [Display(Name = "Progress Hnadled")]

    DocumentHandled = 5,

    /// <summary>
    /// 提醒：任务进度已更新（可用于跟进状态变更）
    /// </summary>
    [Display(Name = "Progress Update")]
    ProgressUpdated = 6,

    /// <summary>
    /// 系统通用提醒（如系统维护、公告等）
    /// </summary>
    [Display(Name = "General System Alert")]
    GeneralSystemAlert = 99
}
