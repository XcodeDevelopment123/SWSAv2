using System.Text.Json.Serialization;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Submissions;

namespace SWSA.MvcPortal.Models.CompnayWorks;

//Use for map to submission detail
public class CompanyWorkFullVM : CompanyWorkVM
{
    public virtual AnnualReturnSubmissionVM? ARSubmission { get; set; } = null!;
    public virtual CompanyStrikeOffSubmissionVM? StrikeOffSubmission { get; set; } = null!;
    public virtual AuditSubmissionVM? AuditSubmission { get; set; } = null!;
    public virtual LLPSubmission? LLPSubmission { get; set; } = null!;

    public void MapSubmissionDetail()
    {
        SubmissionDetail = WorkType switch
        {
            WorkType.AnnualReturn => ARSubmission,
            WorkType.StrikeOff => StrikeOffSubmission,
            WorkType.Audit => AuditSubmission,
            WorkType.LLP => LLPSubmission,
            _ => null
        };
    }
}

public class CompanyWorkVM
{
    public int TaskId { get; set; }
    public int CompanyId { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public CompanyStatus CompanyStatus { get; set; }
    public WorkType WorkType { get; set; }
    public CompanyActivityLevel ActivitySize { get; set; }
    public string? InternalNote { get; set; }
    public bool IsYearEndTask { get; set; }
    public CompanyWorkProgressVM Progress { get; set; } = null!;
    public List<CompanyWorkUserVM> AssignedUsers { get; set; } = new List<CompanyWorkUserVM>();
    public DateTime? ReminderDate { get; set; }
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

    public string GetSubmissionPageLink()
    {
        if (SubmissionDetail is not BaseSubmissionVM baseVM)
            return "#";

        return WorkType switch
        {
            WorkType.StrikeOff => $"/secretary-dept/submissions/company-strike-off/{baseVM.SubmissionId}/edit",
            WorkType.Audit => $"/secretary-dept/submissions/audit-report/{baseVM.SubmissionId}/edit",
            WorkType.AnnualReturn => $"/secretary-dept/submissions/annual-return/{baseVM.SubmissionId}/edit",
            WorkType.LLP => $"/secretary-dept/submissions/llp/{baseVM.SubmissionId}/edit",
            _ => "#"
        };
    }
}
