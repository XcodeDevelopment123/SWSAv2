using SWSA.MvcPortal.Entities.Clients;

namespace SWSA.MvcPortal.Repositories.Interfaces;

public interface IClientRepository : IRepositoryBase<BaseClient>
{
    Task<bool> CompanyExists(string coNumber, string coName);
    Task<bool> IndividualExists(string icOrPassport);
    IQueryable<BaseClient> Query(List<int>? ids = null);
}
