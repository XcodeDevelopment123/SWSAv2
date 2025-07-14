using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class CreateCompanyWorkAssignmentRequest
{
    public int CompanyId { get; set; }
    public WorkType WorkType { get; set; }
    public bool IsYearEndTask { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public string? InternalNote { get; set; }
    public List<MonthOfYear> Months { get; set; } = new();
    public List<string> AssignedUserIds { get; set; } = new();
    public DateTime? ReminderDate { get; set; }
}

public static class WorkAssignedUsersMapper
{
    public static List<WorkAssignmentUserMapping> ToEntities(List<int> userIds)
    {
        return [.. userIds.Select(userId => new WorkAssignmentUserMapping
        {
            UserId = userId,
        })];
    }
}
