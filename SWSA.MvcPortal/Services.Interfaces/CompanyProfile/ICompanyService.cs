using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Companies;

namespace SWSA.MvcPortal.Services.Interfaces.CompanyProfile;

public interface ICompanyService
{
    Task<List<CompanyListVM>> GetCompanyListVMAsync();
    Task<int> Create(CreateCompanyRequest req);
    Task<Company> GetCompanyByIdAsync(int companyId);
    Task<Company> Delete(int companyId);
    Task<bool> Edit(EditCompanyRequest req);
    Task<List<CompanySelectionVM>> GetCompanySelectionAsync();
    Task<List<CompanyListVM>> GetCompanyListByTypeAsync(CompanyType type);
    Task<CompanySecretaryVM> GetCompanyForSecretaryVMByIdAsync(int companyId);
    Task<CompanySimpleInfoVM> GetCompanySimpleInfoVMByIdAsync(int companyId);
    Task<List<CompanySelectionVM>> GetCompanySelectionByTypeAsync(CompanyType type);
    Task<List<SecretaryCompanyListVM>> GetSecretaryCompanyListVMByYearAsync(int year);
}
