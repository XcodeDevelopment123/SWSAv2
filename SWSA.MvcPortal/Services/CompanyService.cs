using AutoMapper;
using Force.DeepCloner;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Packaging;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Exceptions;
using SWSA.MvcPortal.Commons.Guards;
using SWSA.MvcPortal.Commons.Services.Permission;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Dtos.Requests.Users;
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
IMsicCodeRepository msicCodeRepository,
IUserCompanyDepartmentRepository userCompanyDepartmentRepository,
IUserRepository userRepository,
IUserContext userContext,
ISystemAuditLogService sysAuditService,
IPermissionRefreshTracker permissionRefreshTracker
    ) : ICompanyService
{

    public async Task<List<CompanyListVM>> GetCompaniesAsync()
    {
        var data = userContext.IsSuperAdmin ? await GetCompaniesFromCacheAsync()
            : await repo.GetCompaniesByUserId(userContext.EntityId);
        return mapper.Map<List<CompanyListVM>>(data);
    }

    public async Task<List<CompanyListVM>> GetCompaniesByTypeAsync(CompanyType type)
    {
        var data = userContext.IsSuperAdmin ? await GetCompaniesFromCacheAsync()
            : await repo.GetCompaniesByUserId(userContext.EntityId);

        data = [.. data.Where(data => data.CompanyType == type)];
        return mapper.Map<List<CompanyListVM>>(data);
    }

    public async Task<List<CompanySelectionVM>> GetCompanySelectionAsync()
    {
        var data = userContext.IsSuperAdmin ? await GetCompaniesFromCacheAsync()
            : await repo.GetCompaniesByUserId(userContext.EntityId);
        return mapper.Map<List<CompanySelectionVM>>(data);
    }

    public async Task<Company> GetCompanyByIdAsync(int companyId)
    {
        Guard.AgainstUnauthorizedCompanyAccess(companyId, null, userContext);
        var data = await GetCompanyWithIncludedByIdFromCacheAsync(companyId);
        Guard.AgainstNullData(data, "Company not found");

        return data!;
    }

    public async Task<int> Create(CreateCompanyRequest req)
    {
        if (req.HandleUsers.Count == 0)
            throw new BusinessLogicException("Please select at least one user to handle this company");

        Company cp = mapper.Map<Company>(req);

        await repo.BeginTransactionAsync();
        try
        {
            repo.Add(cp);
            await repo.SaveChangesAsync();

            //Process handle user associated
            var staffIds = req.HandleUsers.Select(x => x.StaffId).ToHashSet();
            var userIds = await userRepository.GetDictionaryIdByStaffIdsAsync([.. staffIds]);
            var allUserCompanyDepartments = new List<UserCompanyDepartment>();
            foreach (var handleUser in req.HandleUsers)
            {
                var userId = userIds[handleUser.StaffId];
                var noRepeatDepartments = handleUser.Departments.Select(x => x).Distinct().ToList();

                var entities = UserCompanyDepartmentMapper.ToEntities(noRepeatDepartments, cp.Id, userId);
                allUserCompanyDepartments.AddRange(entities);

                permissionRefreshTracker.MarkRefreshNeeded(handleUser.StaffId);
            }

            userCompanyDepartmentRepository.AddRange(allUserCompanyDepartments);
            await userCompanyDepartmentRepository.SaveChangesAsync();

            await repo.CommitTransactionAsync();
        }
        catch
        {
            await repo.RollbackTransactionAsync();
            throw;
        }

        ClearCompaniesCache();
        var log = SystemAuditLogEntry.Create(Commons.Enums.SystemAuditModule.Company, cp.Id.ToString(), cp.Name, cp);
        sysAuditService.LogInBackground(log);
        return cp.Id;
    }

    public async Task<bool> Edit(EditCompanyRequest req)
    {
        Guard.AgainstUnauthorizedCompanyAccess(req.CompanyId, null, userContext);

        var data = await GetCompanyWithIncludedByIdFromCacheAsync(req.CompanyId);
        Guard.AgainstNullData(data, "Company not found");
        var oldData = data.DeepClone();

        data!.Name = req.CompanyName;
        data.RegistrationNumber = req.RegistrationNumber;
        data.EmployerNumber = req.EmployerNumber;
        data.TaxIdentificationNumber = req.TaxIdentificationNumber;
        data.YearEndMonth = req.YearEndMonth;
        data.IncorporationDate = req.IncorporationDate;
        data.CompanyType = req.CompanyType;

        await SyncMsicCodes(data, req.MsicCodeIds?.ToHashSet() ?? new());

        repo.Update(data);
        await repo.SaveChangesAsync();
        ClearCompaniesCache(data.Id);

        var log = SystemAuditLogEntry.Update(Commons.Enums.SystemAuditModule.Company, data.Id.ToString(), data.Name, oldData, data);
        sysAuditService.LogInBackground(log);
        return true;
    }

    public async Task<Company> Delete(int companyId)
    {
        Guard.AgainstUnauthorizedCompanyAccess(companyId, null, userContext);

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
}
