using SWSA.MvcPortal.Dtos.Requests.Companies;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface ICompanyOwnerService
{
    Task<int> Create(CreateCompanyOwnerRequest req);
    Task<bool> Delete(int ownerId);
    Task<bool> Edit(EditCompanyOwnerRequest req);
}
