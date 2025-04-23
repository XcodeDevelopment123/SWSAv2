using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.SystemAuditLogs;

public class SystemAuditLogEntry
{
    public SystemAuditModule Module { get; set; } = default!;
    public SystemAuditActionType ActionType { get; set; } = default!;
    public string EntityId { get; set; } = default!;
    public string? EntityName { get; set; }
    public object? OldData { get; set; }
    public object? NewData { get; set; }
    private SystemAuditLogEntry(SystemAuditModule module, SystemAuditActionType actionType, string entityId, string? entityName, object? oldData, object? newData)
    {
        Module = module;
        ActionType = actionType;
        EntityId = entityId;
        EntityName = entityName;
        OldData = oldData;
        NewData = newData;
    }
    public static SystemAuditLogEntry Create(SystemAuditModule module, string entityId, string? entityName, object newData)
    {
        return new SystemAuditLogEntry(module, SystemAuditActionType.Create, entityId, entityName, null, newData);
    }

    public static SystemAuditLogEntry Update(SystemAuditModule module, string entityId, string? entityName, object oldData, object newData)
    {
        return new SystemAuditLogEntry(module, SystemAuditActionType.Update, entityId, entityName, oldData, newData);
    }

    public static SystemAuditLogEntry Delete(SystemAuditModule module, string entityId, string? entityName, object oldData)
    {
        return new SystemAuditLogEntry(module, SystemAuditActionType.Delete, entityId, entityName, oldData, null);
    }
}
