using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Commons.Constants;

public class AppSettings
{
    public const string DbConnString = "SwsaConntection";
    public const string AllowedOrigins = "AllowedOrigins";
    public const string DynamicCorsPolicy = "DynamicCorsPolicy";
    public const string NotAvailable = "N/A";
    public const string DisplayPassword = "••••••";

}


public class SessionKeys
{
    public const string StaffId = "StaffId";
    public const string LoginTime = "LoginTime";
    public const string Name = "Name";
    public const string CompanyId = "CompanyId"; // Use for company staff login 
    public const string CompanyDepartmentId = "CompanyDepartmentId"; // Use for company staff login 
}