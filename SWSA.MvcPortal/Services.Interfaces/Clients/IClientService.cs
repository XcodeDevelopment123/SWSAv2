using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Entities.Clients;

namespace SWSA.MvcPortal.Services.Interfaces.Clients;

public interface IClientService
{
    Task<BaseCompany> CreateCompanyAsync(CreateCompanyRequest req);
    Task<IndividualClient> CreateIndividualAsync(CreateIndividualRequest req);
    Task<BaseClient> GetClientByIdAsync(int id);
    Task<List<T>> GetClientsAsync<T>() where T : BaseClient;
    Task<IEnumerable<object>> SearchClientsAsync(ClientType type, ClientFilterRequest request);
}
