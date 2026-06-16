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

        await Task.WhenAll(tasks);

        return res;
    }

    private async Task LoadGroupsAsync(ClientOptionResponse res, ClientType clientType)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var query = db.Set<BaseClient>().AsQueryable().AsNoTracking();
        //Can do cahing here
        var groups = await query
            .Where(c => c.ClientType == clientType)
            .Select(c => c.Group ?? "")
            .Distinct()
            .OrderBy(c => c)
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
        var baseQuery = db.Set<BaseClient>().AsQueryable().AsNoTracking()
            .Where(c => c.ClientType == clientType)
            .Where(c => c.Referral != null && c.Referral.Trim() != "");

        var referrals = await baseQuery
            .Select(c => c.Referral ?? "")
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();

        res.Referrals = referrals ?? [];

        if (clientType == ClientType.Individual)
            return;

        var companyQuery = db.Set<BaseCompany>().AsQueryable().AsNoTracking()
            .Where(c => c.ClientType == clientType)
            .Where(c => c.Referral != null && c.Referral.Trim() != "");

        var data = await companyQuery
            .GroupBy(c => c.Referral)
            .Select(g => new
            {
                Referral = g.Key ?? "",
                CompanyNo = g.First().RegistrationNumber ?? "",
                IncorpDate = g.First().IncorporationDate
            })
            .ToListAsync();

        res.ReferralCompanyInfoMap = data.ToDictionary(
            x => x.Referral,
            x => new ReferralCompanyInfoDto
            {
                CompanyNumber = x.CompanyNo,
                IncorporationDate = x.IncorpDate?.ToString("yyyy-MM-dd") ?? ""
            }
        );
    }
}
