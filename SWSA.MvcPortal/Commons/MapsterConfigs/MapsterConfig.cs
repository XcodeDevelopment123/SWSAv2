using Mapster;

namespace SWSA.MvcPortal.Commons.MapsterConfigs;

public interface IMapsterConfig
{
    void Register(TypeAdapterConfig config);
}

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        var config = TypeAdapterConfig.GlobalSettings;
        var configs = new List<IMapsterConfig>
        {
            //#Mapster Config (auto generated)
new AnnualReturnSubmissionMapsterConfig(),
new CompanyCommunicationContactMapsterConfig(),
new CompanyComplianceDateMapsterConfig(),
new CompanyMapsterConfig(),
new CompanyMsicCodeMapsterConfig(),
new CompanyOfficialContactMapsterConfig(),
new CompanyOwnerMapsterConfig(),
new CompanyStrikeOffSubmissionMapsterConfig(),
new CompanyWorkAssignmentMapsterConfig(),
new CompanyWorkProgressMapsterConfig(),
new DocumentRecordMapsterConfig(),
new MsicCodeMapsterConfig(),
new ScheduledJobMapsterConfig(),
new SystemAuditLogMapsterConfig(),
new SystemNotificationLogMapsterConfig(),
new UserCompanyDepartmentMapsterConfig(),
new UserMapsterConfig(),
new WorkAssignmentAccountMonthMapsterConfig(),
new WorkAssignmentAuditMonthMapsterConfig(),
new WorkAssignmentUserMappingMapsterConfig(),
            //#Mapster Config end
        };
        foreach (var mapConfig in configs)
            mapConfig.Register(config);
    }
}

