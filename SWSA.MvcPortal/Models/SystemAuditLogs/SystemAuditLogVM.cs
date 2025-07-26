using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.SystemAuditLogs;

public class SystemAuditLogVM
{
    public int LogId { get; set; }
    public SystemAuditModule Module { get; set; }
    public SystemAuditActionType ActionType { get; set; }
    public string EntityId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string PerformedBy { get; set; } = default!;
    public string? PerformedByStaffId { get; set; }
    public DateTime PerformedAt { get; set; }
    public List<ChangeSummaryVM> ChangeSummaries { get; set; }
}
