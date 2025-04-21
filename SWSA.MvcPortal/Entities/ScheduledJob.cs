using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;

public class ScheduledJob
{
    [Key]
    public int Id { get; set; }

    /// <summary>Quartz JobKey, 如 AssignmentDueSoonJob</summary>
    public string JobKey { get; set; } = default!;

    /// <summary>Quartz Job Group，如 NotificationGroup </summary>
    public string JobGroup { get; set; } = default!;

    /// <summary>调度类型：Once, Daily, Weekly, Monthly, Cron</summary>

    public ScheduleType ScheduleType { get; set; } = default!;

    /// <summary>如果是 One-Time 类型，这里是执行时间</summary>
    public DateTime? TriggerTime { get; set; }

    /// <summary>对于 Cron 类型任务，存储 Quartz Cron 表达式</summary>
    public string? CronExpression { get; set; }

    /// <summary>是否启用调度</summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>是否为用户自定义</summary>
    public bool IsCustom { get; set; } = false;

    /// <summary>序列化的请求参数（如 JobRequest）</summary>
    public string? RequestPayloadJson { get; set; }

    /// <summary>任务创建者</summary>
    public string CreatedBy { get; set; } = "System Default"; // if userId not null, use user full name instead of default
    [ForeignKey(nameof(UserId))]
    public int? UserId { get; set; } //If null , that is System Default
    public User? User { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? LastUpdatedAt { get; set;}
}
