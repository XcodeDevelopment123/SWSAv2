using SWSA.MvcPortal.Entities;
using System.CodeDom;

namespace SWSA.MvcPortal.Commons.Constants;

public class AppSettings
{
    public const string DbConnString = "SwsaConntection";
    public const string AllowedOrigins = "AllowedOrigins";
    public const string DynamicCorsPolicy = "DynamicCorsPolicy";
    public const string NotAvailable = "N/A";
    public const string DisplayPassword = "••••••";

}

public class QuartzGroupKeys
{
    public const string NotificationGroup = "notificationGroup";
    public const string ReportGroup = "reportGroup";
}


public class SessionKeys
{
    public const string StaffId = "StaffId";
    public const string LoginTime = "LoginTime";
    public const string Name = "Name";
    public const string CompanyId = "CompanyId"; // Use for company staff login 
    public const string CompanyDepartmentId = "CompanyDepartmentId"; // Use for company staff login 
}

public class FileSettings
{
    public string LocalDomain { get; set; }
    public string CloudDomain { get; set; }
}


public class WappySettings 
{
    public string Url { get; set; } = string.Empty;
    public string ApiToken { get; set; } = string.Empty;


}

public class MessagingTemplateCode
{
    public const string OTP= "OTP";
    public const string AssignmentWorkDueSoon = "Assignment_Work_Due_Soon";
    public const string Notification = "Notification";
}