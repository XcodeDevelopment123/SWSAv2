using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.CompanyWorks;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.WorkAssignments;

namespace SWSA.MvcPortal.Services.WorkAssignment;

public interface IWorkAssignmentFactory
{
    CompanyWorkAssignment Create(WorkAssignmentRequest request, Company cp);
}

public class WorkAssignmentFactory : IWorkAssignmentFactory
{
    public CompanyWorkAssignment Create(WorkAssignmentRequest request, Company cp)
    {
        var baseInfo = new
        {
            request.CompanyId,
            WorkType = request.Type,
            ForYear = request.Year,
            CreatedAt = DateTime.Now,
            ServiceScope = ServiceScope.Other,
            cp.CompanyStatus,
            cp.CompanyActivityLevel
        };

        return request.Type switch
        {
            WorkType.AnnualReturn => new AnnualReturnWorkAssignment
            {
                ForYear = baseInfo.ForYear,
                CompanyId = baseInfo.CompanyId,
                WorkType = baseInfo.WorkType,
                CreatedAt = baseInfo.CreatedAt,
                ServiceScope = baseInfo.ServiceScope,
                CompanyStatus = baseInfo.CompanyStatus,
                CompanyActivityLevel = baseInfo.CompanyActivityLevel,
                DateSubmitted = DateTime.Now,
                AnniversaryDate = cp.IncorporationDate?.AddMonths(17),
                ARDueDate = cp.IncorporationDate?.AddMonths(17).AddDays(30),
            },

            WorkType.Audit => new AuditWorkAssignment
            {
                ForYear = baseInfo.ForYear,
                CompanyId = baseInfo.CompanyId,
                WorkType = baseInfo.WorkType,
                CreatedAt = baseInfo.CreatedAt,
                ServiceScope = baseInfo.ServiceScope,
                CompanyStatus = baseInfo.CompanyStatus,
                CompanyActivityLevel = baseInfo.CompanyActivityLevel,
                DateSubmitted = DateTime.Now,
                TargetedCirculation = cp.YearEndMonth.HasValue
                    ? new DateTime(baseInfo.ForYear, (int)cp.YearEndMonth.Value, 1).AddMonths(7).AddDays(-1)
                    : null,
                FirstYearAccountStart = cp.IncorporationDate
            },

            WorkType.StrikeOff => new StrikeOffWorkAssignment
            {
                ForYear = baseInfo.ForYear,
                CompanyId = baseInfo.CompanyId,
                WorkType = baseInfo.WorkType,
                CreatedAt = baseInfo.CreatedAt,
                ServiceScope = baseInfo.ServiceScope,
                CompanyStatus = baseInfo.CompanyStatus,
                CompanyActivityLevel = baseInfo.CompanyActivityLevel,
                StartDate = DateTime.Now.Date
            },

            WorkType.LLP => new LLPWorkAssignment
            {
                ForYear = baseInfo.ForYear,
                CompanyId = baseInfo.CompanyId,
                WorkType = baseInfo.WorkType,
                CreatedAt = baseInfo.CreatedAt,
                ServiceScope = baseInfo.ServiceScope,
                CompanyStatus = baseInfo.CompanyStatus,
                CompanyActivityLevel = baseInfo.CompanyActivityLevel,

            },

            _ => throw new NotSupportedException($"WorkType '{request.Type}' not supported")
        };
    }
}
