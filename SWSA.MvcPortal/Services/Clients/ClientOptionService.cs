using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Dtos.Responses.Clients;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Services.Clients;

public class ClientOptionService(
  //use context factory to resolve concurency issue
  IDbContextFactory<AppDbContext> dbFactory
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

        if (req.IncludeReferrals)
        {
            tasks.Add(LoadReferralsAsync(res, req.ClientType));
        }

        if (req.IncludeCompanies && req.ClientType != ClientType.Individual)
        {
            tasks.Add(LoadCompaniesAsync(res, req.ClientType));
        }

        await Task.WhenAll(tasks);

        return res;
    }

    private async Task LoadGroupsAsync(ClientOptionResponse res, ClientType clientType)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var groups = await db.Groups.AsNoTracking()
            .Where(g => g.IsActive)
            .OrderBy(g => g.GroupName)
            .Select(g => g.GroupName)
            .Distinct()
            .ToListAsync();

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

    private async Task LoadReferralsAsync(ClientOptionResponse res, ClientType clientType)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var referrals = await db.Referrals.AsNoTracking()
            .Where(r => r.IsActive)
            .OrderBy(r => r.ReferralName)
            .Select(r => r.ReferralName)
            .Distinct()
            .ToListAsync();

        res.Referrals = referrals ?? [];
    }

    private async Task LoadCompaniesAsync(ClientOptionResponse res, ClientType clientType)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var companyQuery = db.Set<BaseCompany>().AsQueryable().AsNoTracking()
            .Where(c => c.ClientType == clientType)
            .Where(c => c.Name != null && c.Name.Trim() != "");

        var companies = await companyQuery
            .OrderBy(c => c.Name)
            .Select(c => new CompanyFilterOptionDto
            {
                Name = c.Name ?? "",
                RegistrationNumber = c.RegistrationNumber ?? ""
            })
            .ToListAsync();

        res.Companies = companies ?? [];
    }
}
