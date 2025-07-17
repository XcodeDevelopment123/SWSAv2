using SWSA.MvcPortal.Dtos.Requests.Contacts;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Services.Interfaces.CompanyProfile;

public interface ICompanyOwnerService
{
    Task<bool> Delete(int id);
    Task<CompanyOwner?> GetByIdAsync(int id);
    Task<CompanyOwner> UpsertContact(UpsertCompanyOwnerRequest req);
}
