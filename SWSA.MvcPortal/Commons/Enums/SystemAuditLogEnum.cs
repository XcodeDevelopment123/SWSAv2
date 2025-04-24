namespace SWSA.MvcPortal.Commons.Enums;

public enum SystemAuditModule
{
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
    Create,
    Update,
    Delete,
}