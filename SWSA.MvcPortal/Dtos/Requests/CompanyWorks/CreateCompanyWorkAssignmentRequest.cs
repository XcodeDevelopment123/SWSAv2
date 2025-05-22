using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Users;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class CreateCompanyWorkAssignmentRequest
{
    public int CompanyId { get; set; }
    public CompanyStatus CompanyStatus { get; set; }
    public WorkType WorkType { get; set; }
    public bool IsYearEndTask { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public CompanyActivityLevel CompanyActivityLevel { get; set; }
    public string? InternalNote { get; set; }
    public WorkAssignmentMonthRequest AuditUsers { get; set; }
    public WorkAssignmentMonthRequest AccountUsers { get; set; }
}

public static class WorkAssignedUsersMapper
{
    public static List<WorkAssignmentUserMapping> ToEntities(List<int> userIds, string department)
    {
        return [.. userIds.Select(userId => new WorkAssignmentUserMapping
        {
            UserId = userId,
            Department = department
        })];
    }
}
