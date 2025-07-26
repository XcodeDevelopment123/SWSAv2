using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.SystemAuditLogs;

public class SystemAuditLogEntry
{
    public SystemAuditModule Module { get; set; } = default!;
    public SystemAuditActionType ActionType { get; set; } = default!;
    public string EntityId { get; set; } = default!;
    public string? Message { get; set; }
    public object? OldData { get; set; }
    public object? NewData { get; set; }
    private SystemAuditLogEntry(SystemAuditModule module, SystemAuditActionType actionType, string entityId, string? message, object? oldData, object? newData)
    {
        Module = module;
        ActionType = actionType;
        EntityId = entityId;
        Message = message;
        OldData = oldData;
        NewData = newData;
    }
    public static SystemAuditLogEntry Create(SystemAuditModule module, string entityId, string? message, object newData)
    {
        return new SystemAuditLogEntry(module, SystemAuditActionType.Create, entityId, message, null, newData);
    }

    public static SystemAuditLogEntry Update(SystemAuditModule module, string entityId, string? message, object oldData, object newData)
    {
        return new SystemAuditLogEntry(module, SystemAuditActionType.Update, entityId, message, oldData, newData);
    }

    public static SystemAuditLogEntry Delete(SystemAuditModule module, string entityId, string? message, object oldData)
    {
        return new SystemAuditLogEntry(module, SystemAuditActionType.Delete, entityId, message, oldData, null);
    }

    public static SystemAuditLogEntry Execute(SystemAuditModule module, string entityId, string? message)
    {
        return new SystemAuditLogEntry(module, SystemAuditActionType.Execute, entityId, message, null, null);
    }
}
