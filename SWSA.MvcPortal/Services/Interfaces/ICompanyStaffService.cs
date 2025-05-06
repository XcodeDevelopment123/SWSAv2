using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Models.CompanyStaffs;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface ICompanyStaffService 
{
    // Define your method here
    Task<int> CreateStaff(CreateCompanyStaffRequest req);
    Task<bool> EditStaff(EditCompanyStaffInfoRequest req);
    Task<bool> DeleteStaff(string staffId);
    Task<List<CompanyStaffVM>> GetStaffsByCompanyId(int companyId);
    Task<CompanyStaffVM> GetStaffByStaffId(string staffId);
}
