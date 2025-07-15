using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Repositories.Interfaces;

namespace SWSA.MvcPortal.Repositories.Repo;

public class ClientRepository(AppDbContext db) : RepositoryBase<BaseClient>(db), IClientRepository
{
    public IQueryable<BaseClient> Query(List<int>? ids = null)
    {
        var query = db.Set<BaseClient>().AsQueryable();

        if (ids != null && ids.Count != 0)
        {
            query = query.Where(c => ids.Contains(c.Id));
        }

        return query;
    }

    public async Task<bool> CompanyExists(string coNumber, string coName)
    {
        return await db.Set<BaseCompany>().AnyAsync(x =>
            x.RegistrationNumber == coNumber && x.Name == coName);
    }

    public async Task<bool> IndividualExists(string icOrPassport)
    {
        return await db.Set<IndividualClient>().AnyAsync(x =>  x.Name == icOrPassport);
    }

}
