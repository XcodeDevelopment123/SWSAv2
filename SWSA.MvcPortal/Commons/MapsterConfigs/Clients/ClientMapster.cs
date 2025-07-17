using Mapster;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Models.Clients;

namespace SWSA.MvcPortal.Commons.MapsterConfigs.Clients;

public class ClientMapster : IMapsterConfig
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<BaseClient, BaseClient>();
        config.ForType<BaseClient, ClientSelectionVM>()
                 .Map(dest => dest.ClientId, src => src.Id)
                 .Map(dest => dest.ClientType, src => src.ClientType)
                 .Map(dest => dest.Name, src => src.Name);
    }
}
