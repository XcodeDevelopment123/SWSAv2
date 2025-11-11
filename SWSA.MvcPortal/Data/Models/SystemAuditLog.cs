using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class SystemAuditLog
{
    public int Id { get; set; }

    public string Module { get; set; } = null!;

    public string ActionType { get; set; } = null!;

    public string EntityId { get; set; } = null!;

    public string? Message { get; set; }

    public string? ChangeSummaryJson { get; set; }

    public string PerformedBy { get; set; } = null!;

    public int? PerformedByUserId { get; set; }

    public DateTime PerformedAt { get; set; }

    public virtual User? PerformedByUser { get; set; }
}
