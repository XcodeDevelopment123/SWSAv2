using Mapster;
using SWSA.MvcPortal.Commons.MapsterConfigs.Clients;
using SWSA.MvcPortal.Commons.MapsterConfigs.CompanyProfile;
using SWSA.MvcPortal.Commons.MapsterConfigs.Contacts;
using SWSA.MvcPortal.Commons.MapsterConfigs.Scheduler;
using SWSA.MvcPortal.Commons.MapsterConfigs.SystemInfra;
using SWSA.MvcPortal.Commons.MapsterConfigs.UserAccess;

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
new ClientMapster(),
new CommunicationContactMapsterConfig(),
new OfficialContactMapsterConfig(),
new CompanyOwnerMapsterConfig(),
new DocumentRecordMapsterConfig(),
new MsicCodeMapsterConfig(),
new ScheduledJobMapsterConfig(),
new SystemAuditLogMapsterConfig(),
new SystemNotificationLogMapsterConfig(),
new UserMapsterConfig(),
            //#Mapster Config end
        };
        foreach (var mapConfig in configs)
            mapConfig.Register(config);
    }
}

