namespace SWSA.MvcPortal.Dtos.Requests.Clients;

public class ClientFilterRequest
{
    public string? Grouping { get; set; }
    public string? Referral { get; set; }
    public string? FileNo { get; set; }
    public string? Name { get; set; }
    public string? CompanyNumber { get; set; }
    public string? Profession { get; set; }

    public DateTime? IncorpDateFrom{ get; set; }
    public DateTime? IncorpDateTo { get; set; }
}
