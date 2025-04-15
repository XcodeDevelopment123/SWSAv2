using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface ICompanyStaffService 
{
    // Define your method here
    Task<int> CreateContact(CreateCompanyStaffRequest req);
    Task<bool> EditContact(EditCompanyStaffInfoRequest req);
    Task<bool> DeleteContact(int ownerId);
    Task<List<CompanyStaff>> GetStaffsByCompanyId(int companyId);
}
