using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Exceptions;
using SWSA.MvcPortal.Commons.Guards;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Dtos.Requests.Contacts;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Models.SystemAuditLogs;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Persistence.QueryExtensions;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Services.Clients;

public class WorkAllocationService(
       AppDbContext db
    ) : IWorkAllocationService
{
    private readonly DbSet<BaseClient> _clients = db.Set<BaseClient>();
    private readonly DbSet<ClientWorkAllocation> _workAllocs = db.Set<ClientWorkAllocation>();

    #region VM/DTO Query Method 

    public async Task<ClientWorkAllocation?> GetByIdAsync(int id)
    {
        var data = await _workAllocs.FirstOrDefaultAsync(c => c.Id == id);
        Guard.AgainstNullData(data, "Work Allocation not found");

        return data;
    }

    #endregion

    public async Task<ClientWorkAllocation> UpsertWorkAlloc(UpsertWorkAllocationRequest req)
    {
        var clientExist = await _clients.ExistsAsync(req.ClientId);
        Guard.AgainstNotExist(clientExist, "Client Not Found : " + req.ClientId);

        var serviceExists = await _workAllocs.ClientServiceExistAsync(req.ClientId, req.Id, req.Service);
        if (serviceExists)
        {
            throw new BusinessLogicException("Service already exists for this client");
        }

        ClientWorkAllocation? entity = null;
        if (req.Id.HasValue && req.Id > 0)
        {
            entity = await _workAllocs.FirstOrDefaultAsync(c => c.Id == req.Id.Value);
        }

        if (entity != null)
        {
            entity.ServiceScope = req.Service;
            entity.Remarks = req.Remarks;
            entity.CompanyActivityLevel = req.ActivitySize;
            entity.AuditStatus = req.AuditStatus;
            entity.CompanyStatus = req.AuditCpStatus;
            _workAllocs.Update(entity);
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
        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        var data = await GetByIdAsync(id);

        db.Remove(data!);
        await db.SaveChangesAsync();

        return true;
    }
}
