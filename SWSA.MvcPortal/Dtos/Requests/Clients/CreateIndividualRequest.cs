using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Clients;

public class CreateIndividualRequest
{
    public string IndividualName { get; set; }
    public string ICOrPassportNumber { get; set; } = null!;
    public string Professions { get; set; }
    public MonthOfYear? YearEndMonth { get; set; }
    public string? TaxIdentificationNumber { get; set; }
    public ClientType ClientType { get; set; }
    public ClientCategoryRequest CategoryInfo { get; set; }
}
