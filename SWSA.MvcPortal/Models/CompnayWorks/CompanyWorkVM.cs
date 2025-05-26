using Mapster;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Extensions;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Submissions;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkVM
{
    public int TaskId { get; set; }
    public int CompanyId { get; set; }
    public CompanyStatus CompanyStatus { get; set; }
    public WorkType WorkType { get; set; }
    public CompanyActivityLevel ActivitySize { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public string? InternalNote { get; set; }
    public bool IsYearEndTask { get; set; }
    public CompanyWorkProgressVM Progress { get; set; } = null!;
    public List<CompanyWorkUserVM> AssignedUsers { get; set; } = new List<CompanyWorkUserVM>();
    public List<MonthOfYear> PlannedMonths { get; set; } = new List<MonthOfYear>();
    public object? SubmissionDetail { get; set; } = null!; // This will be the specific submission type based on WorkType
    public void MergeUsers()
    {
        var mergedResult = AssignedUsers.GroupBy(x => x.StaffId)
                  .Select(g =>
                  {
                      var first = g.First();
                      return new CompanyWorkUserVM
                      {
                          StaffId = first.StaffId,
                          StaffName = first.StaffName,
                          Role = first.Role,
                      };
                  }).ToList();

        AssignedUsers = mergedResult;
    }

    public object MapDetailDto(CompanyWorkAssignment src)
    {
        switch (src.WorkType)
        {
            case WorkType.AnnualReturn:
                return src.ARSubmission.Adapt<AnnualReturnSubmissionVM>();
            case WorkType.StrikeOff:
                return src.StrikeOffSubmission.Adapt<CompanyStrikeOffSubmissionVM>();
            // ...
            default:
                return null!;
        }
    }
}
