using SWSA.MvcPortal.Dtos.Requests.Companies;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface ICompanyOwnerService
{
    Task<int> CreateOwner(CreateCompanyOwnerRequest req);
    Task<bool> DeleteOwner(int ownerId);
    Task<bool> EditOwner(EditCompanyOwnerRequest req);
}
