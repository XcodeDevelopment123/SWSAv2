using Force.DeepCloner;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Exceptions;
using SWSA.MvcPortal.Commons.Extensions;
using SWSA.MvcPortal.Commons.Guards;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Entities.WorkAllocations;
using SWSA.MvcPortal.Models.SystemAuditLogs;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Persistence.QueryExtensions;
using SWSA.MvcPortal.Services.Interfaces.Clients;
using SWSA.MvcPortal.Services.Interfaces.Systems;

namespace SWSA.MvcPortal.Services.Clients;

public class WorkAllocationService(
    ISystemAuditLogService sysAuditService,
    AppDbContext db
    ) : IWorkAllocationService
{
    private readonly DbSet<BaseClient> _clients = db.Set<BaseClient>();
    private readonly DbSet<ClientWorkAllocation> _workAllocs = db.Set<ClientWorkAllocation>();

    #region VM/DTO Query Method 

    public async Task<ClientWorkAllocation?> GetByIdAsync(int id)
    {
        var data = await _workAllocs.FirstOrDefaultAsync(c => c.Id == id);
        return data;
    }

    public async Task<List<ClientWorkAllocation>> GetWorksByClientId(int clientId)
    {
        var data = await _workAllocs.Where(c => c.ClientId == clientId).OrderByDescending(c => c.ServiceScope).ToListAsync();
        return data;
    }

    #endregion

    public async Task<ClientWorkAllocation> UpsertWorkAlloc(UpsertWorkAllocationRequest req)
    {
        var clientExist = await _clients.ExistsAsync(req.ClientId);
        Guard.AgainstNotExist(clientExist, "Client Not Found : " + req.ClientId);

        var serviceExists = await _workAllocs.ClientServiceExistAsync(req.ClientId, req.Id, req.Service);
        Guard.AgainstExist(serviceExists,"Service already exists for this client");

        ClientWorkAllocation? entity = null;
        if (req.Id.HasValue && req.Id > 0)
        {
            entity = await _workAllocs.FirstOrDefaultAsync(c => c.Id == req.Id.Value);
        }

        SystemAuditLogEntry? log = null;

        if (entity != null)
        {
            var oldData = entity.DeepClone();
            entity.ChangeServiceScope(req.Service);
            entity.UpdateInfo(entity.Remarks, req.ActivitySize, req.AuditCpStatus, req.AuditStatus);
            _workAllocs.Update(entity);
            log = SystemAuditLogEntry.Update(SystemAuditModule.WorkAllocation, entity.Id.ToString(), $"Work Allocation: {entity.ServiceScope.GetDisplayName()}", oldData, entity);

        }
        else
        {
            entity = new ClientWorkAllocation
            {
                ClientId = req.ClientId,
                ServiceScope = req.Service,
                Remarks = req.Remarks,
                CompanyActivityLevel = req.ActivitySize,
                AuditStatus = req.AuditStatus,
                CompanyStatus = req.AuditCpStatus,
            };
            await _workAllocs.AddAsync(entity);
        }

        await db.SaveChangesAsync();

        log ??= SystemAuditLogEntry.Create(SystemAuditModule.WorkAllocation, entity.Id.ToString(), $"Work Allocation: {entity.ServiceScope.GetDisplayName()}", entity);
        sysAuditService.LogInBackground(log);
        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        var data = await GetByIdAsync(id);
        Guard.AgainstNullData(data, "Work Allocation not found with id: " + id);

        db.Remove(data!);
        await db.SaveChangesAsync();

        var log = SystemAuditLogEntry.Delete(SystemAuditModule.WorkAllocation, data.Id.ToString(), $"Work Allocation: {data.ServiceScope.GetDisplayName()}", data);
        sysAuditService.LogInBackground(log);
        return true;
    }

}
