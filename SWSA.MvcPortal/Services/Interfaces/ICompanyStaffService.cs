using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Models.CompanyStaffs;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface ICompanyStaffService 
{
    // Define your method here
    Task<int> CreateContact(CreateCompanyStaffRequest req);
    Task<bool> EditContact(EditCompanyStaffInfoRequest req);
    Task<bool> DeleteContact(string staffId);
    Task<List<CompanyStaffVM>> GetStaffsByCompanyId(int companyId);
    Task<bool> EditLoginProfile(EditCompanyStaffLoginProfileRequest req);
    Task<CompanyStaffVM> GetStaffByStaffId(string staffId);
    Task<bool> SetStaffSession(string staffId);
}
