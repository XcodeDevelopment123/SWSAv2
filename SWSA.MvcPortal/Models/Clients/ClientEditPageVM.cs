using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.Clients;

namespace SWSA.MvcPortal.Models.Clients;

public class ClientEditPageVM
{
    public List<MsicCode> MsicCodes { get; set; } = [];

    public ClientEditPageVM(List<MsicCode> msicCodes)
    {
        MsicCodes = msicCodes;
    }
}

public class CompanyClientEditPageVM<T>(List<MsicCode> msicCodes, T client) : ClientEditPageVM(msicCodes)
{
    public T CompanyClient { get; set; } = client;
}

public class IndividualClientEditPageVM
{
    public IndividualClient IndividualClient { get; set; }

    public IndividualClientEditPageVM(IndividualClient client)
    {
        IndividualClient = client;
    }
}