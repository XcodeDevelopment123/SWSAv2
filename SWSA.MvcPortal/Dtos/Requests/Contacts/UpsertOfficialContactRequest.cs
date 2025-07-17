
namespace SWSA.MvcPortal.Dtos.Requests.Contacts;

public class UpsertOfficialContactRequest
{
    public int? Id { get; set; } //entity id, if have value is update
    public int ClientId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Remark { get; set; }
}
