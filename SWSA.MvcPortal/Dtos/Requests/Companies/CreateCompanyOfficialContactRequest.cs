namespace SWSA.MvcPortal.Dtos.Requests.Companies;

public class CreateCompanyOfficialContactRequest
{
    public int? CompanyId { get; set; }
    public string Address { get; set; } = null!;
    public string OfficeTel { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Remark { get; set; } = null!;
}
