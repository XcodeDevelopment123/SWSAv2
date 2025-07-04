using SWSA.MvcPortal.Dtos.Requests.Companies;

namespace SWSA.MvcPortal.Services.Interfaces.CompanyProfile;

public interface ICompanyCommunicationContactService 
{
    // Define your method here
    Task<int> Create(CreateCompanyCommunicationContactRequest req);
    Task<bool> Edit(EditCompanyCommunicationContactRequest req);
    Task<bool> Delete(int id);
}
