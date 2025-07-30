
namespace SWSA.MvcPortal.Commons.Constants;

public class AppSettings
{
    public const string DbConnString = "SwsaConntection";
    public const string AllowedOrigins = "AllowedOrigins";
    public const string DynamicCorsPolicy = "DynamicCorsPolicy";
    public const string NotAvailable = "N/A";
    public const string DisplayPassword = "••••••";

}

public class AppModule
{
    public const string WorkAssignment = "WorkAssignment";
    public const string Company = "Company";
    public const string User = "User";
    public const string Submission = "Submission";
    public const string System = "System";
    public const string Scheduler = "Scheduler";
}



public class QuartzGroupKeys
{
    public const string NotificationGroup = "notificationGroup";
    public const string ReportGroup = "reportGroup";
}


public class SessionKeys
{
    public const string EntityId = "EntityId";
    public const string StaffId = "StaffId";
    public const string LoginTime = "LoginTime";
    public const string Name = "Name";
    public const string UserRole = "UserRole";
    public const string AllowedCompanyIds = "AllowedCompanyIds";
    public const string AllowedDepartments = "AllowedDepartments";

}

public class FileSettings
{
    public string LocalDomain { get; set; } = string.Empty;
    public string CloudDomain { get; set; } = string.Empty;
}



public class MessagingTemplateCode
{
    public const string OTP = "OTP";
    public const string AssignmentWorkDueSoon = "Assignment_Work_Due_Soon";
    public const string AssignmentRemind = "Assignment_Remind";
    public const string Notification = "Notification";
}