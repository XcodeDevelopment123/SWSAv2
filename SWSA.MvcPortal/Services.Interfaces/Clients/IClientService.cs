using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Models.Clients;

namespace SWSA.MvcPortal.Services.Interfaces.Clients;

public interface IClientService
{
    Task<BaseCompany> CreateCompanyAsync(CreateCompanyRequest req);
    Task<IndividualClient> CreateIndividualAsync(CreateIndividualRequest req);
    Task<T> GetClientByIdAsync<T>(int id) where T : BaseClient;
    Task<List<T>> GetClientsAsync<T>() where T : BaseClient;
    Task<List<ClientSelectionVM>> GetClientSelectionVM(List<ClientType> types);
    Task<BaseClient> GetClientWithDetailByIdAsync(int id);
    Task<BaseClient> GetEditClientByIdAsync(int id);
    Task<IEnumerable<object>> SearchClientsAsync(ClientType type, ClientFilterRequest request);
    Task<BaseCompany> UpdateCompanyAsync(UpdateCompanyRequest req);
    Task<IndividualClient> UpdateIndividualAsync(UpdateIndividualRequest req);
    Task<List<SdnBhdOptionDto>> GetAllSdnBhdOptionsAsync();
    Task<List<CompanyOptionDto>> GetCompanyOptionsAsync();
    Task<List<CompanyOptionDto>> GetLlpCompanyOptionsAsync();
}
