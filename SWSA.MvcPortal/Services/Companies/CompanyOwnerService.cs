using Force.DeepCloner;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Guards;
using SWSA.MvcPortal.Dtos.Requests.Contacts;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Models.SystemAuditLogs;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Persistence.QueryExtensions;
using SWSA.MvcPortal.Services.Interfaces.Companies;
using SWSA.MvcPortal.Services.Interfaces.SystemInfra;

namespace SWSA.MvcPortal.Services.Companies;

public class CompanyOwnerService(
    ISystemAuditLogService sysAuditService,
    AppDbContext db
    ) : ICompanyOwnerService
{
    private readonly DbSet<BaseClient> _clients = db.Set<BaseClient>();

    private readonly DbSet<CompanyOwner> _owners = db.Set<CompanyOwner>();

    #region VM/DTO Query Method 
    public async Task<CompanyOwner?> GetByIdAsync(int id)
    {
        var data = await _owners.FirstOrDefaultAsync(c => c.Id == id);
        return data;
    }
    #endregion

    public async Task<CompanyOwner> UpsertContact(UpsertCompanyOwnerRequest req)
    {
        var clientExist = await _clients.ExistsAsync(req.ClientId);
        Guard.AgainstNotExist(clientExist, "Client Not Found : " + req.ClientId);

        CompanyOwner? entity = null;
        if (req.Id.HasValue)
        {
            entity = await _owners.FirstOrDefaultAsync(c => c.Id == req.Id.Value);
        }

        SystemAuditLogEntry? log = null;

        if (entity != null)
        {
            var oldData = entity.DeepClone();
            entity.NamePerIC = req.Name;
            entity.ICOrPassportNumber = req.ICOrPassport;
            entity.Position = req.Position;
            entity.TaxReferenceNumber = req.TaxRef;
            entity.Email = req.Email;
            entity.PhoneNumber = req.PhoneNumber;
            entity.RequiresFormBESubmission = req.IsRequireSubmitFormBE;
            _owners.Update(entity);
            log = SystemAuditLogEntry.Update(SystemAuditModule.CompanyOwner, entity.Id.ToString(), $"Owner: {entity.NamePerIC}", oldData, entity);
        }
        else
        {
            entity = new CompanyOwner
            {
                ClientCompanyId = req.ClientId,
                NamePerIC = req.Name,
                ICOrPassportNumber = req.ICOrPassport,
                Position = req.Position,
                TaxReferenceNumber = req.TaxRef,
                Email = req.Email,
                PhoneNumber = req.PhoneNumber,
                RequiresFormBESubmission = req.IsRequireSubmitFormBE,
            };
            await _owners.AddAsync(entity);
        }

        await db.SaveChangesAsync();

        log ??= SystemAuditLogEntry.Create(SystemAuditModule.CompanyOwner, entity.Id.ToString(), $"Owner: {entity.NamePerIC}", entity);
        sysAuditService.LogInBackground(log);
        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        var data = await GetByIdAsync(id);
        Guard.AgainstNullData(data, "Company owner not found");

        db.Remove(data!);
        await db.SaveChangesAsync();

        var log = SystemAuditLogEntry.Delete(SystemAuditModule.CompanyOwner, data.Id.ToString(), $"Owner: {data.NamePerIC}", data);
        sysAuditService.LogInBackground(log);
        return true;
    }
}
