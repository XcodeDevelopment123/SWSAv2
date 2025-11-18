namespace SWSA.MvcPortal.Commons.Constants;
public class ControllerNames
{
    public const string Dashboard = "Dashboard";
    public const string Client = "Client";
    public const string Company = "Company";
    public const string NewClient = "NewClient";
    public const string Secretary = "Secretary";
    public const string CompanyStaff = "CompanyStaff";
    public const string User = "User";
    public const string Document = "Document";
    public const string CompanyWork = "CompanyWork";
    public const string Notification = "Notification";
    public const string SystemAuditLog = "SystemAuditLog";
    public const string SchedulerJob = "SchedulerJob";
    public const string Group = "Group";

}

public class ControllerInfo
{
    public string Name { get; }

    public ControllerInfo(string? name)
    {
        Name = name ?? string.Empty;
    }

    public static implicit operator string(ControllerInfo controller)
    {
        return controller.Name;
    }

    public bool Is(string target)
    {
        return string.Equals(target, Name, StringComparison.OrdinalIgnoreCase);
    }
}

public class ActionInfo
{
    public string Name { get; }
    public ActionInfo(string? name)
    {
        Name = name ?? string.Empty;
    }

    public bool Is(string target)
    {
        return string.Equals(target, Name, StringComparison.OrdinalIgnoreCase);
    }
}