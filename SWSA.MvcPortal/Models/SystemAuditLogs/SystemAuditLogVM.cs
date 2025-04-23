using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.SystemAuditLogs;

public class SystemAuditLogVM
{
    public SystemAuditModule Module { get; set; }
    public SystemAuditActionType ActionType { get; set; }
    public string EntityId { get; set; } = default!;
    public string EntityName { get; set; } = default!;
    public string? NavigateUrl { get; set; }
    public string PerformedBy { get; set; } = default!;
    public string? PerformedByStaffId { get; set; }
    public DateTime PerformedAt { get; set; }
    public List<ChangeSummaryVM> ChangeSummaries { get; set; }
}
