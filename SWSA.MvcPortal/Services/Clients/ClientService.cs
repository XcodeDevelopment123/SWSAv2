using Force.DeepCloner;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Exceptions;
using SWSA.MvcPortal.Commons.Guards;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Models.Clients;
using SWSA.MvcPortal.Models.SystemAuditLogs;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Persistence.QueryExtensions;
using SWSA.MvcPortal.Services.Interfaces.Clients;
using SWSA.MvcPortal.Services.Interfaces.SystemInfra;

namespace SWSA.MvcPortal.Services.Clients;

public class ClientService(
   IClientCreationFactory _clientCreationFactory,
   AppDbContext db,
   ISystemAuditLogService _sysAuditService
    ) : IClientService
{
    private readonly DbSet<BaseClient> _clients = db.Set<BaseClient>();
    private readonly DbSet<MsicCode> _msicCodeds = db.Set<MsicCode>();
    private readonly DbSet<CompanyMsicCode> _cpMsicCodeds = db.Set<CompanyMsicCode>();
    private readonly DbSet<BaseCompany> _companies = db.Set<BaseCompany>();
    private readonly DbSet<IndividualClient> _individualClients = db.Set<IndividualClient>();

    #region Get Data
    public async Task<IEnumerable<object>> SearchClientsAsync(ClientType type, ClientFilterRequest request)
    {
        return type switch
        {
            ClientType.SdnBhd => await _companies.OfType<SdnBhdClient>().ApplyFilter(request).ToListAsync(),
            ClientType.LLP => await _companies.OfType<LLPClient>().ApplyFilter(request).ToListAsync(),
            ClientType.Enterprise => await _companies.OfType<EnterpriseClient>().ApplyFilter(request).ToListAsync(),
            ClientType.Individual => await _individualClients.ApplyFilter(request).ToListAsync(),
            _ => throw new ArgumentException($"Unsupported client type: {type}")
        };
    }

    public async Task<List<ClientSelectionVM>> GetClientSelectionVM(List<ClientType> types)
    {
        if (types.Count == 0)
        {
            return await _clients.ProjectToType<ClientSelectionVM>().ToListAsync();
        }

        return await _clients.Where(c => types.Contains(c.ClientType))
            .ProjectToType<ClientSelectionVM>().ToListAsync();
    }

    public async Task<List<T>> GetClientsAsync<T>() where T : BaseClient
    {
        var data = await _clients.OfType<T>().ToListAsync();

        return data;
    }

    public async Task<BaseClient> GetClientWithDetailByIdAsync(int id)
    {

        var clientType = await _clients
            .Where(c => c.Id == id)
            .Select(c => c.ClientType)
            .FirstOrDefaultAsync();

        IQueryable<BaseClient> query = null!;

        query = clientType switch
        {
            ClientType.Individual => _individualClients
                                .Include(c => c.CommunicationContacts)
                                .Include(c => c.OfficialContacts)
                                .Include(c => c.WorkAllocations),

            ClientType.Enterprise
            or ClientType.SdnBhd
            or ClientType.LLP => _companies
                                .Include(c => c.MsicCodes).ThenInclude(c => c.MsicCode)
                                .Include(c => c.CommunicationContacts)
                                .Include(c => c.OfficialContacts)
                                .Include(c => c.Owners)
                                .Include(c => c.WorkAllocations),
            _ => throw new InvalidOperationException("Unknown client type"),
        };
        var result = await query.FirstOrDefaultAsync(c => c.Id == id);
        Guard.AgainstNullData(result, "Client Not Found");

        return result!;
    }

    public async Task<T> GetClientByIdAsync<T>(int id) where T : BaseClient
    {
        var data = await _clients.OfType<T>().FirstOrDefaultAsync(c => c.Id == id);
        Guard.AgainstNullData(data, "Client Not Found");

        return data!;
    }

    public async Task<BaseClient> GetEditClientByIdAsync(int id)
    {
        var clientType = await _clients
            .Where(c => c.Id == id)
            .Select(c => c.ClientType)
            .FirstOrDefaultAsync();

        IQueryable<BaseClient> query = null!;

        query = clientType switch
        {
            ClientType.Individual => _individualClients,

            ClientType.Enterprise
            or ClientType.SdnBhd
            or ClientType.LLP => _companies
                                .Include(c => c.MsicCodes).ThenInclude(c => c.MsicCode),
            _ => throw new InvalidOperationException("Unknown client type"),
        };
        var result = await query.FirstOrDefaultAsync(c => c.Id == id);
        Guard.AgainstNullData(result, "Client Not Found");

        return result!;
    }
    #endregion

    public async Task<BaseCompany> CreateCompanyAsync(CreateCompanyRequest req)
    {
        var isExist = await _companies.CompanyExistsAsync(req.RegistrationNumber, req.CompanyName);
        if (isExist)
        {
            throw new BusinessLogicException("Company Name / Number already exists");
        }

        var entity = _clientCreationFactory.CreateCompanyAsync(req);

        await db.AddAsync(entity);
        await db.SaveChangesAsync();

        var log = SystemAuditLogEntry.Create(SystemAuditModule.Client, entity.Id.ToString(), $"Client: {entity.Name}", entity);
        _sysAuditService.LogInBackground(log);
        return entity;
    }

    public async Task<IndividualClient> CreateIndividualAsync(CreateIndividualRequest req)
    {
        var isExist = await _individualClients.IcOrPassportExistsAsync(req.ICOrPassportNumber);
        if (isExist)
        {
            throw new BusinessLogicException("IC/Passport already exists");
        }

        var entity = _clientCreationFactory.CreateIndividualAsync(req);
        await db.AddAsync(entity);
        await db.SaveChangesAsync();

        var log = SystemAuditLogEntry.Create(SystemAuditModule.Client, entity.Id.ToString(), $"Client: {entity.Name}", entity);
        _sysAuditService.LogInBackground(log);
        return entity;
    }

    public async Task<BaseCompany> UpdateCompanyAsync(UpdateCompanyRequest req)
    {
        var isExist = await _companies.CompanyExistsAsync(req.ClientId, req.CompanyName, req.RegistrationNumber);
        if (isExist)
        {
            throw new BusinessLogicException("The new Company Name / Number already exists");
        }

        var entity = await _companies
            .Include(c => c.MsicCodes)
            .ThenInclude(c => c.MsicCode)
            .FirstOrDefaultAsync(c => c.Id == req.ClientId);

        Guard.AgainstNullData(entity, "Client Not Found");

        var oldData = entity.DeepClone();

        entity!.Group = req.CategoryInfo?.Group;
        entity!.Referral = req.CategoryInfo?.Referral;
        entity!.FileNo = req.CategoryInfo?.FileNo;
        entity!.Name = req.CompanyName;
        entity.RegistrationNumber = req.RegistrationNumber;
        entity.TaxIdentificationNumber = req.TaxIdentificationNumber;
        entity.EmployerNumber = req.EmployerNumber;
        entity.YearEndMonth = req.YearEndMonth;
        entity.IncorporationDate = req.IncorporationDate;

        await SyncMsicCodes(entity, req.MsicCodeIds?.ToHashSet() ?? []);

        db.Update(entity);
        await db.SaveChangesAsync();

        var log = SystemAuditLogEntry.Update(SystemAuditModule.Client, entity.Id.ToString(), $"Client: {entity.Name}", oldData, entity);
        _sysAuditService.LogInBackground(log);
        return entity;
    }

    public async Task<IndividualClient> UpdateIndividualAsync(UpdateIndividualRequest req)
    {
        var isExist = await _individualClients.IcOrPassportExistsAsync(req.ClientId, req.ICOrPassportNumber);
        if (isExist)
        {
            throw new BusinessLogicException("The new IC/Passport already exists");
        }

        var entity = await _individualClients.FirstOrDefaultAsync(c => c.Id == req.ClientId);
        Guard.AgainstNullData(entity, "Client Not Found");

        var oldData = entity.DeepClone();

        entity!.Group = req.CategoryInfo?.Group;
        entity!.Referral = req.CategoryInfo?.Referral;

        entity!.Name = req.IndividualName;
        entity.ICOrPassportNumber = req.ICOrPassportNumber;
        entity.TaxIdentificationNumber = req.TaxIdentificationNumber;
        entity.Profession = req.Professions;
        entity.YearEndMonth = req.YearEndMonth;
        entity.TaxIdentificationNumber = req.TaxIdentificationNumber;

        db.Update(entity);
        await db.SaveChangesAsync();

        var log = SystemAuditLogEntry.Update(SystemAuditModule.Client, entity.Id.ToString(), $"Client: {entity.Name}", oldData, entity);
        _sysAuditService.LogInBackground(log);
        return entity;
    }

    private async Task SyncMsicCodes(BaseCompany data, HashSet<int> requestedMsicIds)
    {
        var existingMsicIds = data.MsicCodes.Select(x => x.MsicCodeId).ToHashSet();
        var msicIdsToAdd = requestedMsicIds.Except(existingMsicIds).ToList();
        var msicsToRemove = data.MsicCodes
          .Where(x => !requestedMsicIds.Contains(x.MsicCodeId)).ToList();
        var msicIdsToRemove = msicsToRemove.Select(x => x.MsicCodeId).ToList();

        if (msicIdsToAdd.Count > 0)
        {
            var msicEntities = await _msicCodeds.GetMsicCodes(msicIdsToAdd).ToListAsync();
            data.MsicCodes.AddRange(msicEntities.Select(msic => new CompanyMsicCode(msic.Id)));
        }

        foreach (var msic in msicsToRemove)
        {
            //Remove in data entity
            data.MsicCodes.Remove(msic);
        }

        //Remove in db
        var msicRemoveEntities = await _cpMsicCodeds.GetMsicCodes(msicIdsToRemove).ToListAsync();
        _cpMsicCodeds.RemoveRange(msicRemoveEntities);
    }
}
