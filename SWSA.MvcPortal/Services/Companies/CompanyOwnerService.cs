using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Guards;
using SWSA.MvcPortal.Dtos.Requests.Contacts;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Persistence.QueryExtensions;
using SWSA.MvcPortal.Services.Interfaces.CompanyProfile;

namespace SWSA.MvcPortal.Services.Companies;

public class CompanyOwnerService(
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

        if (entity != null)
        {
            entity.NamePerIC = req.Name;
            entity.ICOrPassportNumber = req.ICOrPassport;
            entity.Position = req.Position;
            entity.TaxReferenceNumber = req.TaxRef;
            entity.Email = req.Email;
            entity.PhoneNumber = req.PhoneNumber;
            entity.RequiresFormBESubmission = req.IsRequireSubmitFormBE;
            _owners.Update(entity);
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
        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        var data = await GetByIdAsync(id);
        Guard.AgainstNullData(data, "Company owner not found");

        db.Remove(data!);
        await db.SaveChangesAsync();

        return true;
    }
}
