
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Repositories.Interfaces;

public interface ICompanyMsicCodeRepository : IRepositoryBase<CompanyMsicCode>
{
    // Define your method here
    Task<List<CompanyMsicCode>> GetByCompanyId(int companyId);
    Task<List<CompanyMsicCode>> GetByIdsAsync(List<int> msicCodeIds);
}
