using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Models.Entities;

public partial class ScheduledJob
{
    public int Id { get; set; }

    public string JobKey { get; set; } = null!;

    public string JobGroup { get; set; } = null!;

    public int JobType { get; set; }

    public int ScheduleType { get; set; }

    public DateTime? TriggerTime { get; set; }

    public string? CronExpression { get; set; }

    public bool IsEnabled { get; set; }

    public bool IsCustom { get; set; }

    public string? RequestPayloadJson { get; set; }

    public DateTime? LastExecuteAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public int? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; }
}
