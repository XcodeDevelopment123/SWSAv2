using Newtonsoft.Json;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SWSA.MvcPortal.Entities;

[Module("Scheduler")]

public class ScheduledJob
{
    [Key]
    public int Id { get; set; }

    [SystemAuditLog("Job Key")]
    /// <summary>Quartz JobKey, 如 AssignmentDueSoonJob</summary>
    public string JobKey { get; set; } = default!;
   
    [SystemAuditLog("Job Group")]
    /// <summary>Quartz Job Group，如 NotificationGroup </summary>
    public string JobGroup { get; set; } = default!;

    [SystemAuditLog("Job Type")]
    public ScheduledJobType JobType { get; set; }

    [SystemAuditLog("Schedule Type")]

    /// <summary>调度类型：Once, Daily, Weekly, Monthly, Cron</summary>
    public ScheduleType ScheduleType { get; set; } = default!;

    [SystemAuditLog("Trigger Time")]
    /// <summary>如果是 One-Time 类型，这里是执行时间</summary>
    public DateTime? TriggerTime { get; set; }

    [SystemAuditLog("Cron Expression")]
    /// <summary>对于 Cron 类型任务，存储 Quartz Cron 表达式</summary>
    public string? CronExpression { get; set; }

    [SystemAuditLog("Is Enabled")]
    /// <summary>是否启用调度</summary>
    public bool IsEnabled { get; set; } = true;

    [SystemAuditLog("Is Custom Job")]
    /// <summary>是否为用户自定义</summary>
    public bool IsCustom { get; set; } = false;

    /// <summary>序列化的请求参数（如 JobRequest）</summary>
    public string? RequestPayloadJson { get; set; }
    public DateTime? LastExecuteAt { get; set; }

    [SystemAuditLog("Created By")]
    /// <summary>任务创建者</summary>
    public string CreatedBy { get; set; } = "System Default"; // if userId not null, use user full name instead of default
    [ForeignKey(nameof(UserId))]
    public int? UserId { get; set; } //If null , that is System Default
    public User? User { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}

public static class ScheduledJobExtensions
{
    private static readonly HashSet<ScheduledJobType> _jobTypesWithExtraParams = new()
    {
        ScheduledJobType.GenerateAssignmentReport,
    };

    public static bool RequiresExtraPayload(this ScheduledJob job)
        => _jobTypesWithExtraParams.Contains(job.JobType);

    /// <summary>判断 Payload 是否为指定类型且所有非 Base 属性已填写</summary>
    public static bool HasValidPayload<T>(this ScheduledJob job) where T : BaseJobRequest, new()
    {
        if (string.IsNullOrWhiteSpace(job.RequestPayloadJson)) return false;

        T? payload;
        try
        {
            payload = JsonConvert.DeserializeObject<T>(job.RequestPayloadJson);
        }
        catch
        {
            return false;
        }

        if (payload == null) return false;

        // 获取当前类的属性（排除继承自 BaseJobRequest 的属性）
        var extraProperties = typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

        foreach (var prop in extraProperties)
        {
            var value = prop.GetValue(payload);

            // 判断空值（可调整规则）
            if (value == null) return false;

            // 针对数字和枚举再加强判断
            if (prop.PropertyType.IsValueType && Equals(value, Activator.CreateInstance(prop.PropertyType)))
                return false;
        }

        return true;
    }
}