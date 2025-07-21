using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Clients;

public  class ClientOptionRequest
{
    public bool IsValid => IncludeGroups || IncludeProfessions;

    public ClientType ClientType { get; set; }
    public bool IncludeProfessions { get; set; }
    public bool IncludeGroups { get; set; }
}
