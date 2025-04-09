using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Guards;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Services;

public class CompanyOwnerService(
IMemoryCache cache,
MemoryCacheEntryOptions cacheOptions,
IMapper mapper,
ICompanyOwnerRepository repo
    ) : ICompanyOwnerService
{
    public async Task<int> CreateOwner(CreateCompanyOwnerRequest req)
    {
        var data = mapper.Map<CompanyOwner>(req);
        repo.Add(data);
        await repo.SaveChangesAsync();
        UpdateCompanyOwnerCache(data);
        return data.Id;
    }

    public async Task<bool> EditOwner(EditCompanyOwnerRequest req)
    {
        var data = await repo.GetByIdAsync(req.OwnerId);

        Guard.AgainstNullData(data, "Company Owner not found");

        if (data!.CompanyId != req.CompanyId)
        {
            return false;
        }

        data.NamePerIC = req.NamePerIC;
        data.ICOrPassportNumber = req.ICOrPassportNumber;
        data.Position = req.Position;
        data.TaxReferenceNumber = req.TaxReferenceNumber;
        data.Email = req.Email;
        data.PhoneNumber = req.PhoneNumber;
        data.OwnershipType = req.OwnershipType;

        repo.Update(data);
        await repo.SaveChangesAsync();
        UpdateCompanyOwnerCache(data);
        return true;
    }

    public async Task<bool> DeleteOwner(int ownerId)
    {
        var data = await repo.GetByIdAsync(ownerId);
        Guard.AgainstNullData(data, "Company Owner not found");

        repo.Remove(data!);
        await repo.SaveChangesAsync();
        UpdateCompanyOwnerCache(data!, true);
        return true;
    }


    private void UpdateCompanyOwnerCache(CompanyOwner owner, bool isRemove = false)
    {
        string cacheList = $"{DataCacheKey.Companies}";
        string cacheDetailKey = $"{DataCacheKey.Companies}_{owner.CompanyId}_Details";
        cache.Remove(cacheList);
        cache.Remove(cacheDetailKey);
        //TO DO: update company dto and find index update owner 

    }
}
