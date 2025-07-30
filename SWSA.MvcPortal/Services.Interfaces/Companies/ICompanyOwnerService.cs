using SWSA.MvcPortal.Dtos.Requests.Contacts;
using SWSA.MvcPortal.Entities.Contacts;

namespace SWSA.MvcPortal.Services.Interfaces.Companies;

public interface ICompanyOwnerService
{
    Task<bool> Delete(int id);
    Task<CompanyOwner?> GetByIdAsync(int id);
    Task<CompanyOwner> UpsertContact(UpsertCompanyOwnerRequest req);
}
