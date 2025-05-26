using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class EditCompanyWorkAssignmentRequest
{
    public int TaskId { get; set; }
    public CompanyStatus CompanyStatus { get; set; }
    public bool IsYearEndTask { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public CompanyActivityLevel CompanyActivityLevel { get; set; }
    public string? InternalNote { get; set; }
}
