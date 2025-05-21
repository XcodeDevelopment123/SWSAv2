using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Models.CompanyStaffs;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface ICompanyCommunicationService 
{
    // Define your method here
    Task<int> Create(CreateCompanyCommunicationContactRequest req);
    Task<bool> Edit(EditCompanyCommunicationContactRequest req);
    Task<bool> Delete(int id);
    Task<List<CompanyCommunicationContactVM>> GetCommunicationContactsByCompanyId(int companyId);
}
