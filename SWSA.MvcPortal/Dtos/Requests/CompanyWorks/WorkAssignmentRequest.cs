using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class WorkAssignmentRequest
{
    public int ClientId { get; set; }
    public WorkType Type { get; set; }
    public int Year { get; set; }
}
