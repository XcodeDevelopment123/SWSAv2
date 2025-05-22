using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyWorks;

public class WorkAssignmentMonthRequest
{
    public List<MonthOfYear> Months { get; set; } = new();
    public List<string> StaffIds { get; set; } = new();

    public List<WorkAssignmentAccountMonth> GetAccountMonthEntities()
    {
        return [.. Months
            .Select(month => new WorkAssignmentAccountMonth
            {
                Month = month,
            })];
    }

    public List<WorkAssignmentAuditMonth> GetAuditMonthEntities()
    {
        return [.. Months
            .Select(month => new WorkAssignmentAuditMonth
            {
                Month = month,
            })];
    }
}
