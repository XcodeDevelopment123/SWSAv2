using SWSA.MvcPortal.Dtos.Requests.Companies;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface ICompanyCommunicationContactService 
{
    // Define your method here
    Task<int> CreateContact(CreateCompanyCommunicationContactRequest req);
    Task<bool> EditContact(EditCompanyCommunicationContactRequest req);
    Task<bool> DeleteContact(int ownerId);
}
