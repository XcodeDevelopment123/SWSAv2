namespace SWSA.MvcPortal.Models.Companies;

public class CompanySelectionVM
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = null!;
    public string? RegistrationNumber { get; set; }
}
