using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Entities.WorkAllocations;

namespace SWSA.MvcPortal.Services.Interfaces.Clients;

public interface IWorkAllocationService
{
    Task<bool> Delete(int id);
    Task<ClientWorkAllocation?> GetByIdAsync(int id);
    Task<ClientWorkAllocation> UpsertWorkAlloc(UpsertWorkAllocationRequest req);
}
