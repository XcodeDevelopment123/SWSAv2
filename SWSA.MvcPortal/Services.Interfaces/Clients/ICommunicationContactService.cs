using SWSA.MvcPortal.Dtos.Requests.Contacts;
using SWSA.MvcPortal.Entities.Contacts;

namespace SWSA.MvcPortal.Services.Interfaces.Clients;

public interface ICommunicationContactService 
{
    // Define your method here
    Task<bool> Delete(int id);
    Task<CommunicationContact> UpsertContact(UpsertCommunicationContactRequest req);
    Task<CommunicationContact?> GetByIdAsync(int id);
}
