using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Packaging;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Guards;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Models.SystemAuditLogs;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Services.Interfaces;
namespace SWSA.MvcPortal.Services;

public class CompanyService(
IMemoryCache cache,
MemoryCacheEntryOptions cacheOptions,
IMapper mapper,
ICompanyRepository repo,
ICompanyMsicCodeRepository companyMsicCodeRepository,
ICompanyDepartmentRepository companyDepartmentRepository,
IDepartmentRepository departmentRepository,
IMsicCodeRepository msicCodeRepository,
IUserContext userContext,
ISystemAuditLogService sysAuditService
    ) : ICompanyService
{

    public async Task<List<CompanyListVM>> GetCompaniesAsync()
    {
        if (userContext.IsCompanyStaff && userContext.CompanyId.HasValue)
        {
            var data = await GetCompanyByIdFromCacheAsync(userContext.CompanyId.Value);
            return mapper.Map<List<CompanyListVM>>(new List<Company>() { data! });
        }
        else
        {
            var data = await GetCompaniesFromCacheAsync();
            return mapper.Map<List<CompanyListVM>>(data);
        }
    }

    public async Task<List<CompanySelectionVM>> GetCompanySelectionAsync()
    {
        if (userContext.IsCompanyStaff && userContext.CompanyId.HasValue)
        {
            var data = await GetCompanyByIdFromCacheAsync(userContext.CompanyId.Value);
            return mapper.Map<List<CompanySelectionVM>>(new List<Company>() { data! });
        }
        else
        {
            var data = await GetCompaniesFromCacheAsync();
            return mapper.Map<List<CompanySelectionVM>>(data);
        }
    }

    public async Task<Company> GetCompanyByIdAsync(int companyId)
    {
        Guard.AgainstCrossCompanyAccess(companyId, userContext);

        var data = await GetCompanyWithIncludedByIdFromCacheAsync(companyId);
        Guard.AgainstNullData(data, "Company not found");

        return data!;
    }

    public async Task<int> CreateCompany(CreateCompanyRequest req)
    {
        Guard.AgainstCompanyStaff(userContext);

        Company cp = mapper.Map<Company>(req);

        repo.Add(cp);
        await repo.SaveChangesAsync();
        ClearCompaniesCache();

        var log = SystemAuditLogEntry.Create(Commons.Enums.SystemAuditModule.Company, cp.Id.ToString(), cp.Name, cp);
        sysAuditService.LogInBackground(log);
        return cp.Id;
    }

    public async Task<bool> UpdateCompanyInfo(EditCompanyRequest req)
    {
        var data = await GetCompanyWithIncludedByIdFromCacheAsync(req.CompanyId);
        Guard.AgainstNullData(data, "Company not found");
        var oldData = mapper.Map<Company>(data);

        data!.Name = req.CompanyName;
        data.RegistrationNumber = req.RegistrationNumber;
        data.EmployerNumber = req.EmployerNumber;
        data.TaxIdentificationNumber = req.TaxIdentificationNumber;
        data.YearEndMonth = req.YearEndMonth;
        data.IncorporationDate = req.IncorporationDate;
        data.Status = req.Status;
        data.CompanyType = req.CompanyType;

        await SyncDepartments(data, req.DepartmentsIds?.ToHashSet() ?? new());
        await SyncMsicCodes(data, req.MsicCodeIds?.ToHashSet() ?? new());

        repo.Update(data);
        await repo.SaveChangesAsync();
        ClearCompaniesCache(data.Id);

        var log = SystemAuditLogEntry.Update(Commons.Enums.SystemAuditModule.Company, data.Id.ToString(), data.Name, oldData, data);
        sysAuditService.LogInBackground(log);
        return true;
    }

    public async Task<Company> DeleteCompanyByIdAsync(int companyId)
    {
        Guard.AgainstCompanyStaff(userContext);

        var data = await GetCompanyByIdFromCacheAsync(companyId);
        Guard.AgainstNullData(data, "Company not found");

        ////Remove in db
        //await repo.BeginTransactionAsync();
        //repo.Remove(data!);
        //await repo.SaveChangesAsync();

        //await repo.CommitTransactionAsync();

        //soft delete
        data!.IsDeleted = true;
        repo.Update(data);
        await repo.SaveChangesAsync();

        ClearCompaniesCache();

        var log = SystemAuditLogEntry.Delete(Commons.Enums.SystemAuditModule.Company, data.Id.ToString(), data.Name, data);
        sysAuditService.LogInBackground(log);
        return data!;
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

    private async Task SyncMsicCodes(Company data, HashSet<int> requestedMsicIds)
    {
        var existingMsicIds = data.MsicCodes.Select(x => x.MsicCodeId).ToHashSet();
        var msicIdsToAdd = requestedMsicIds.Except(existingMsicIds).ToList();
        var msicsToRemove = data.MsicCodes
          .Where(x => !requestedMsicIds.Contains(x.MsicCodeId)).ToList();
        var msicIdsToRemove = msicsToRemove.Select(x => x.MsicCodeId).ToList();


        if (msicIdsToAdd.Count > 0)
        {
            var msicEntities = await msicCodeRepository.GetByIdsAsync(msicIdsToAdd);
            data.MsicCodes.AddRange(msicEntities.Select(msic => new CompanyMsicCode(msic.Id)));
        }

        foreach (var msic in msicsToRemove)
        {
            //Remove in data entity
            data.MsicCodes.Remove(msic);
        }

        //Remove in db
        var msicRemoveEntities = await companyMsicCodeRepository.GetByIdsAsync(msicIdsToRemove);
        companyMsicCodeRepository.RemoveRange(msicRemoveEntities);
    }

    private async Task SyncDepartments(Company company, HashSet<int> requestDepartmentIds)
    {
        var companyDepartments = company.Departments;
        var existingDepartmentIds = companyDepartments.Select(x => x.DepartmentId).ToHashSet();

        var departmentIdsToAdd = requestDepartmentIds.Except(existingDepartmentIds).ToList();
        var departmentIdsToRemove = existingDepartmentIds.Except(requestDepartmentIds).ToList();
        var departmentIdsToRetain = existingDepartmentIds.Intersect(requestDepartmentIds).ToList();

        //Only these new department that not in company
        if (departmentIdsToAdd.Count > 0)
        {
            var departmentsToAddEntities = await departmentRepository.GetByIdsAsync(departmentIdsToAdd) ?? new List<Department>();

            foreach (var dept in departmentsToAddEntities)
            {
                companyDepartments.Add(new CompanyDepartment(dept.Id));
            }
        }

        var deptMap = companyDepartments.ToDictionary(x => x.DepartmentId);
        foreach (var deptId in departmentIdsToRetain)
        {
            if (deptMap.TryGetValue(deptId, out var dept))
                dept.SetActive();
        }

        foreach (var deptId in departmentIdsToRemove)
        {
            if (deptMap.TryGetValue(deptId, out var dept))
                dept.Deactivate();
        }

    }
}
