using SWSA.MvcPortal.Commons.Filters;

namespace SWSA.MvcPortal.Commons.Enums;

public enum SystemAuditModule
{
    [EnumIgnore]
    Unknown = 0,
    ScheduleJob,
    Company,
    CompanyComplianceDate,
    CompanyOfficialContact,
    CompanyOwner,
    CompanyStaff,
    CompanyWorkAssignment,
    DocumentRecord,
    User
}

public enum SystemAuditActionType
{
    [EnumIgnore]
    Unknown = 0,
    Create,
    Update,
    Delete,
    Execute
}