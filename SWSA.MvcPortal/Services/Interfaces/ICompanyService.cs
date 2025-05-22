using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Companies;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface ICompanyService
{
    Task<List<CompanyListVM>> GetCompaniesAsync();
    Task<int> Create(CreateCompanyRequest req);
    Task<Company> GetCompanyByIdAsync(int companyId);
    Task<Company> Delete(int companyId);
    Task<bool> Edit(EditCompanyRequest req);
    Task<List<CompanySelectionVM>> GetCompanySelectionAsync();
    Task<List<CompanyListVM>> GetCompaniesByTypeAsync(CompanyType type);
    Task<CompanySecretaryVM> GetCompanyForSecretaryVMByIdAsync(int companyId);
}
