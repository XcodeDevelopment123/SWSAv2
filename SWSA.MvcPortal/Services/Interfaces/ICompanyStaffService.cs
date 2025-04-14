using SWSA.MvcPortal.Dtos.Requests.Companies;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface ICompanyStaffService 
{
    // Define your method here
    Task<int> CreateContact(CreateCompanyStaffRequest req);
    Task<bool> EditContact(EditCompanyStaffInfoRequest req);
    Task<bool> DeleteContact(int ownerId);
}
