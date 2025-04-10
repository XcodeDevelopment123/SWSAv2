using System;
using System.Threading.Tasks;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Repositories.Interfaces;

namespace SWSA.MvcPortal.Repositories.Interfaces;

public interface IMsicCodeRepository : IRepositoryBase<MsicCode>
{
    // Define your method here
    Task<List<MsicCode>> GetByIdsAsync(List<int> msicCodeIds);
}
