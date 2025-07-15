using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Dtos.Responses.Clients;

namespace SWSA.MvcPortal.Services.Interfaces.Clients;

public interface IClientOptionService
{
  Task<ClientOptionResponse?> GetOptionValuesAsync(ClientOptionRequest req);
}
