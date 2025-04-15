using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class EditCompanyWorkAssignmentRequest
{
    public int TaskId { get; set; }
    public WorkType WorkType { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public CompanyActivityLevel CompanyActivityLevel { get; set; }
    public int YearToDo { get; set; }
    public MonthOfYear MonthToDo { get; set; }
    public string AssignedStaffId { get; set; }
    public string? InternalNote { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedDate { get; set; }
}
