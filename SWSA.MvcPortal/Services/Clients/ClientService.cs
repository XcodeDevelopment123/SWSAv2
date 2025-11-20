using Dapper;
using Force.DeepCloner;
using Mapster;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Exceptions;
using SWSA.MvcPortal.Commons.Guards;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Entities.Systems;
using SWSA.MvcPortal.Models.Clients;
using SWSA.MvcPortal.Models.SystemAuditLogs;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Persistence.QueryExtensions;
using SWSA.MvcPortal.Services.Interfaces.Clients;
using SWSA.MvcPortal.Services.Interfaces.Systems;

namespace SWSA.MvcPortal.Services.Clients;

public class ClientService(
   IClientCreationFactory _clientCreationFactory,
   AppDbContext db,
   ISystemAuditLogService _sysAuditService,
   IConfiguration _configuration
    ) : IClientService
{
    private readonly DbSet<BaseClient> _clients = db.Set<BaseClient>();
    private readonly DbSet<MsicCode> _msicCodeds = db.Set<MsicCode>();
    private readonly DbSet<BaseCompany> _companies = db.Set<BaseCompany>();
    private readonly DbSet<IndividualClient> _individualClients = db.Set<IndividualClient>();
    private readonly string _connectionString = _configuration.GetConnectionString("SwsaConntection");  // ⭐ 获取连接字符串


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
                                .Include(c => c.OfficialContacts),

            ClientType.Enterprise
            or ClientType.SdnBhd
            or ClientType.LLP => _companies
                                .Include(c => c.MsicCodes).ThenInclude(c => c.MsicCode)
                                .Include(c => c.CommunicationContacts)
                                .Include(c => c.OfficialContacts)
                                .Include(c => c.Owners),
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
        Guard.AgainstExist(isExist, "Company Name / Number already exists");

        // ===== 使用 Dapper 处理 GroupId =====
        string? groupName = null;
        if (req.CategoryInfo?.GroupId.HasValue == true && req.CategoryInfo.GroupId.Value > 0)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT Id, GroupName, IsActive FROM dbo.Groups WHERE Id = @GroupId";
            var group = await connection.QueryFirstOrDefaultAsync<GroupInfoDto>(
                sql,
                new { GroupId = req.CategoryInfo.GroupId.Value }
            );

            if (group == null)
                throw new BusinessLogicException("Selected group not found.");

            if (!group.IsActive)
                throw new BusinessLogicException("Selected group is not active.");

            groupName = group.GroupName;

            // 将 GroupName 设置到 CategoryInfo.Group
            req.CategoryInfo.Group = groupName;
        }
        // ===== 结束 =====

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
        Guard.AgainstExist(isExist, "IC/Passport already exists");

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
        Guard.AgainstExist(isExist, "The new Company Name / Number already exists");

        var entity = await _companies
            .Include(c => c.MsicCodes)
            .ThenInclude(c => c.MsicCode)
            .FirstOrDefaultAsync(c => c.Id == req.ClientId);
        Guard.AgainstNullData(entity, "Client Not Found");

        var oldData = entity.DeepClone();

        // ===== 处理 GroupId（如果有变更）=====
        string? groupName = req.CategoryInfo?.Group;

        if (req.CategoryInfo?.GroupId.HasValue == true && req.CategoryInfo.GroupId.Value > 0)
        {
            // 如果前端传了 GroupId，需要验证并获取 GroupName
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT Id, GroupName, IsActive FROM dbo.Groups WHERE Id = @GroupId";
            var group = await connection.QueryFirstOrDefaultAsync<GroupInfoDto>(
                sql,
                new { GroupId = req.CategoryInfo.GroupId.Value }
            );

            if (group == null)
                throw new BusinessLogicException("Selected group not found.");

            if (!group.IsActive)
                throw new BusinessLogicException("Selected group is not active.");

            groupName = group.GroupName;
        }
        // ===== 结束 =====

        entity.UpdateCompanyInfo(req.CompanyName, req.RegistrationNumber,req.ActivitySize, req.TaxIdentificationNumber, req.EmployerNumber, req.YearEndMonth, req.IncorporationDate);
        entity.GroupId = req.CategoryInfo?.GroupId;
        entity.UpdateAdminInfo(groupName, req.CategoryInfo?.Referral, req.CategoryInfo?.FileNo);
        entity.SyncMsicCode(req.MsicCodeIds);

        var currentMsicCodesId = entity.MsicCodes.Select(m => m.MsicCodeId).ToList();
        var validIds = await _msicCodeds
                 .Where(m => currentMsicCodesId.Contains(m.Id))
                 .Select(m => m.Id)
                 .ToListAsync();

        if (validIds.Count != currentMsicCodesId.Count)
            throw new BusinessLogicException("Invalid MSIC Code(s) provided");

        db.Update(entity);
        await db.SaveChangesAsync();

        var log = SystemAuditLogEntry.Update(SystemAuditModule.Client, entity.Id.ToString(), $"Client: {entity.Name}", oldData, entity);
        _sysAuditService.LogInBackground(log);
        return entity;
    }

    public async Task<IndividualClient> UpdateIndividualAsync(UpdateIndividualRequest req)
    {
        var isExist = await _individualClients.IcOrPassportExistsAsync(req.ClientId, req.ICOrPassportNumber);
        Guard.AgainstExist(isExist, "The new IC/Passport already exists");

        var entity = await _individualClients.FirstOrDefaultAsync(c => c.Id == req.ClientId);
        Guard.AgainstNullData(entity, "Client Not Found");

        var oldData = entity.DeepClone();

        entity.UpdateAdminInfo(req.CategoryInfo?.Group, req.CategoryInfo?.Referral);
        entity.UpdateClientInfo(req.IndividualName, req.TaxIdentificationNumber, req.YearEndMonth, req.ICOrPassportNumber, req.Professions);

        db.Update(entity);
        await db.SaveChangesAsync();

        var log = SystemAuditLogEntry.Update(SystemAuditModule.Client, entity.Id.ToString(), $"Client: {entity.Name}", oldData, entity);
        _sysAuditService.LogInBackground(log);
        return entity;
    }
    public async Task<List<SdnBhdOptionDto>> GetAllSdnBhdOptionsAsync()
    {
        return await _companies
            .OfType<SdnBhdClient>()                // ★ 先筛成 SdnBhdClient 子类
            .Where(c => c.ClientType == ClientType.SdnBhd)
            .Select(c => new SdnBhdOptionDto
            {
                Id = c.Id,
                Name = c.Name,                     // 或 c.CompanyName，看你实体属性
                IncorporationDate = c.IncorporationDate
            })
            .AsNoTracking()
            .ToListAsync();
    }

    private class GroupInfoDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public async Task<List<CompanyOptionDto>> GetCompanyOptionsAsync()
    {
        // 如果你有特定 filter（比如只要 active），可以在这里设置
        var filter = new ClientFilterRequest
        {
            // IsActive = true,
            // 其他过滤条件按你现有的来
        };

        // 1️⃣ 先用 SearchClientsAsync 拿到所有 SdnBhd
        var raw = await SearchClientsAsync(ClientType.SdnBhd, filter);

        // 2️⃣ 把 object 转回 SdnBhdClient，然后投影成我们要的 DTO
        return raw
            .Cast<SdnBhdClient>()
            .Select(c => new CompanyOptionDto
            {
                Id = c.Id,
                Grouping = c.Group,                  // 如果是导航对象就用 c.Group.GroupName
                FileNo = c.FileNo,
                Referral = c.Referral,
                CompanyName = c.Name,
                CompanyNo = c.RegistrationNumber,
                IncorporationDate = c.IncorporationDate,
                YearEndMonth = c.YearEndMonth.HasValue
                    ? c.YearEndMonth.Value.ToString()   // "December"
                    : string.Empty
            })
            .ToList();
    }


}
