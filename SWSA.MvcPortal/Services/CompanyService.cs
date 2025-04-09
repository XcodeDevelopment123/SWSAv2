using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Guards;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Services;

public class CompanyService(
IMemoryCache cache,
MemoryCacheEntryOptions cacheOptions,
IMapper mapper,
ICompanyRepository repo,
ICompanyTypeRepository companyTypeRepository,
IDepartmentRepository departmentRepository,
IMsicCodeRepository msicCodeRepository
    ) : ICompanyService
{

    public async Task<List<CompanyListVM>> GetCompaniesAsync()
    {
        var data = await GetCompaniesFromCacheAsync();
        return mapper.Map<List<CompanyListVM>>(data);
    }

    public async Task<Company> GetCompanyByIdAsync(int companyId)
    {
        var data = await GetCompanyWithIncludedByIdFromCacheAsync(companyId);
        Guard.AgainstNullData(data, "Company not found");
        return data!;
    }

    public async Task<Company> DeleteCompanyByIdAsync(int companyId)
    {
        var data = await GetCompanyByIdFromCacheAsync(companyId);
        Guard.AgainstNullData(data, "Company not found");

        repo.Remove(data!);

        await repo.SaveChangesAsync();
        ClearCompaniesCache();
        return data!;
    }

    public async Task<int> CreateCompany(CreateCompanyRequest req)
    {
        Company cp = mapper.Map<Company>(req);

        repo.Add(cp);
        await repo.SaveChangesAsync();

        ClearCompaniesCache();
        return cp.Id;
    }

    private async Task<List<Company>> GetCompaniesFromCacheAsync()
    {
        if (cache.TryGetValue(DataCacheKey.Companies, out List<Company>? companies))
        {
            return companies ?? [];
        }

        // Fetch data from the database or any other source
        companies = [.. await repo.GetAllAsync()];
        if (companies.Count > 0)
        {
            cache.Set(DataCacheKey.Companies, companies, cacheOptions);
        }
        return companies;
    }

    private async Task<Company?> GetCompanyByIdFromCacheAsync(int id)
    {
        string cacheKey = $"{DataCacheKey.Companies}_{id}";
        string cacheDetailKey = $"{cacheKey}_Details";
        if (cache.TryGetValue(cacheDetailKey, out Company? cpDetails))
        {
            return cpDetails;
        }

        if (cache.TryGetValue(cacheKey, out Company? cp))
        {
            return cp;
        }
        // Fetch data from the database or any other source
        cp = await repo.GetByIdAsync(id);
        if (cp != null)
        {
            cache.Set(cacheKey, cp, cacheOptions);
        }
        return cp;
    }

    private async Task<Company?> GetCompanyWithIncludedByIdFromCacheAsync(int id)
    {
        string cacheKey = $"{DataCacheKey.Companies}_{id}";
        string cacheDetailKey = $"{cacheKey}_Details";

        if (cache.TryGetValue(cacheDetailKey, out Company? cp))
        {
            return cp;
        }
        // Fetch data from the database or any other source
        cp = await repo.GetWithIncludedByIdAsync(id);
        if (cp != null)
        {
            cache.Set(cacheDetailKey, cp, cacheOptions);
            //remove old and no detail cache
            cache.Remove(cacheKey);
        }
        return cp;
    }

    private void ClearCompaniesCache(int? id = 0)
    {
        string cacheKey = $"{DataCacheKey.Companies}";
        cache.Remove(cacheKey);
        if (id.HasValue && id > 0)
        {
            cache.Remove($"{cacheKey}_{id}");
            cache.Remove($"{cacheKey}_{id}_Details");
        }
    }

}
