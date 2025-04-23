using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

public class SystemAuditLog
{
    [Key]
    public int Id { get; set; }
    public string Module { get; set; } = default!;        // 模块名，例如 "Job", "Voucher", Use SystemAuditModule enum
    public string ActionType { get; set; } = default!;    // "Create", "Update", "Delete", Use SystemAuditActionType enum
    public string EntityId { get; set; } = default!;       // 实体主键值（纯 string，不绑定外键）
    public string? EntityName { get; set; } = default!;
    public string? ChangeSummaryJson { get; set; }     // 字段变化，如 {"Status": ["Old", "New"]}
    public string? NavigateUrl { get; set; }               // 可导航详情页路径，如 "/jobs/details/abc"
    public string PerformedBy { get; set; } = default!;
    public int? PerformedByUserId { get; set; }
    public int? PerformedByCompanyStaffId { get; set; }
    public int? CompanyId { get; set; }
    public DateTime PerformedAt { get; set; } = DateTime.Now;

    public User? PerformedByUser { get; set; }
    public CompanyStaff? PerformedByCompanyStaff { get; set; }
    public Company? Company { get; set; }
}
