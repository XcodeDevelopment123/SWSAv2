namespace SWSA.MvcPortal.Dtos.Responses.Clients;

public class ClientOptionResponse
{
    public List<string> Professions { get; set; }
    public List<string> Groups { get; set; }
    public List<string> Referrals { get; set; }
    public Dictionary<string, ReferralCompanyInfoDto> ReferralCompanyInfoMap { get; set; }
}

public class ReferralCompanyInfoDto
{
    public string CompanyNumber { get; set; } = "";
    public string IncorporationDate { get; set; } = "";
}
