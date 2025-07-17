using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.Clients;

public class ClientSelectionVM
{
    public int ClientId { get; set; }
    public string Name { get; set; } = null!;
    public ClientType ClientType { get; set; }
}
