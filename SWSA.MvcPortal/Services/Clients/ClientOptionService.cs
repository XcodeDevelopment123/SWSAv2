using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Dtos.Responses.Clients;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Services.Clients;

public class ClientOptionService(
  IDbContextFactory<AppDbContext> dbFactory
    //use context factory to resolve concurency issue
    ) : IClientOptionService
{
    public async Task<ClientOptionResponse?> GetOptionValuesAsync(ClientOptionRequest req)
    {
        if (!req.IsValid)
            return null;

        var res = new ClientOptionResponse();
        var tasks = new List<Task>();

        if (req.IncludeGroups)
        {
            tasks.Add(LoadGroupsAsync(res, req.ClientType));
        }

        if (req.IncludeProfessions && req.ClientType == ClientType.Individual)
        {
            tasks.Add(LoadProfessionsAsync(res));
        }

        await Task.WhenAll(tasks);

        return res;
    }

    private async Task LoadGroupsAsync(ClientOptionResponse res, ClientType clientType)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var query = db.Set<BaseClient>().AsQueryable().AsNoTracking();
        //Can do cahing here
        var groups = clientType switch
        {
            ClientType.LLP => await query.OfType<LLPClient>()
            .Select(c => c.Group ?? "")
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync(),
            ClientType.Enterprise => await query.OfType<EnterpriseClient>()
            .Select(c => c.Group ?? "")
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync(),
            ClientType.SdnBhd => await query.OfType<SdnBhdClient>()
            .Select(c => c.Group ?? "")
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync(),
            ClientType.Individual => await query.OfType<IndividualClient>()
            .Select(c => c.Group ?? "")
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync(),
            _ => []
        };

        res.Groups = groups ?? [];
    }

    private async Task LoadProfessionsAsync(ClientOptionResponse res)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var query = db.Set<BaseClient>().AsQueryable().AsNoTracking();

        //Can do cahing here
        var data = await query.OfType<IndividualClient>()
            .Select(c => c.Profession)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();

        res.Professions = data;
    }
}
