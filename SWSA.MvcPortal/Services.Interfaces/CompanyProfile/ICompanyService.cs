using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Companies;

namespace SWSA.MvcPortal.Services.Interfaces.CompanyProfile;

public interface ICompanyService
{
    Task<int> Create(CreateCompanyRequest req);
    Task<Company> GetCompanyByIdAsync(int companyId);
    Task<Company> Delete(int companyId);
    Task<bool> Edit(EditCompanyRequest req);
    Task<List<CompanySelectionVM>> GetCompanySelectionAsync();
    Task<List<Company>> GetCompaniesByTypeAsync(ClientType type);
    Task<CompanySimpleInfoVM> GetCompanySimpleInfoVMByIdAsync(int companyId);
    Task<List<CompanySelectionVM>> GetCompanySelectionByTypeAsync(ClientType type);
    Task<List<Company>> GetCompaniesAsync();
}
