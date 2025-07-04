using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class WorkAssignmentRequest
{
    public int CompanyId { get; set; }
    public WorkType Type { get; set; }
    public int Year { get; set; }
}
