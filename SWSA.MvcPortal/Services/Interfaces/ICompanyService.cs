using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Companies;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface ICompanyService
{
    Task<List<CompanyListVM>> GetCompaniesAsync();
    Task<int> CreateCompany(CreateCompanyRequest req);
    Task<Company> GetCompanyByIdAsync(int companyId);
    Task<Company> DeleteCompanyByIdAsync(int companyId);
}
