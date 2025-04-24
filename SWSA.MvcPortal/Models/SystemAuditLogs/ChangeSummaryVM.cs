namespace SWSA.MvcPortal.Models.SystemAuditLogs;

public class ChangeSummaryVM
{
    public string FieldName { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }

    public ChangeSummaryVM(string fieldName, string? oldValue, string? newValue)
    {
        FieldName = fieldName;
        OldValue = oldValue;
        NewValue = newValue;
    }
}

