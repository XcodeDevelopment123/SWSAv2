using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Contacts;

public class UpsertCommunicationContactRequest
{
    public int? Id { get; set; } //entity id, if have value is update
    public int ClientId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public PositionType Position { get; set; }
    public string? Remark { get; set; }

}
