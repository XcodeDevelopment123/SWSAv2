using SWSA.MvcPortal.Commons.Attributes;

namespace SWSA.MvcPortal.Commons.Enums;

public enum SystemAuditModule
{
    [EnumIgnore]
    Unknown = 0,
    ScheduleJob,
    Company,

    CompanyComplianceDate,
    CompanyOwner,
    CompanyWorkAssignment,
    DocumentRecord,
    User,

    Client,
    OfficialContact,
    CommunicationContact,
    WorkAllocation,
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