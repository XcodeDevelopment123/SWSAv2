using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.CompanyWorks;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Entities.WorkAssignments;

namespace SWSA.MvcPortal.Services.WorkAssignments;

public interface IWorkAssignmentFactory
{
    Entities.WorkAssignment Create(WorkAssignmentRequest request, BaseClient cp);
}

public class WorkAssignmentFactory : IWorkAssignmentFactory
{
    public Entities.WorkAssignment Create(WorkAssignmentRequest request, BaseClient cp)
    {
        var baseInfo = new
        {
            request.CompanyId,
            WorkType = request.Type,
            ForYear = request.Year,
            CreatedAt = DateTime.Now,
            ServiceScope = ServiceScope.Other,
        };

        return request.Type switch
        {
            WorkType.AnnualReturn => new AnnualReturnWorkAssignment
            {
                ForYear = baseInfo.ForYear,
                ClientId = baseInfo.CompanyId,
                WorkType = baseInfo.WorkType,
                CreatedAt = baseInfo.CreatedAt,
                ServiceScope = baseInfo.ServiceScope,
                DateSubmitted = DateTime.Now,
            },

            WorkType.Audit => new AuditWorkAssignment
            {
                ClientId = baseInfo.CompanyId,
                ForYear = baseInfo.ForYear,
                WorkType = baseInfo.WorkType,
                CreatedAt = baseInfo.CreatedAt,
                ServiceScope = baseInfo.ServiceScope,
                DateSubmitted = DateTime.Now,
                TargetedCirculation = cp.YearEndMonth.HasValue
                    ? new DateTime(baseInfo.ForYear, (int)cp.YearEndMonth.Value, 1).AddMonths(7).AddDays(-1)
                    : null,
            },

            WorkType.StrikeOff => new StrikeOffWorkAssignment
            {
                ForYear = baseInfo.ForYear,
                ClientId = baseInfo.CompanyId,
                WorkType = baseInfo.WorkType,
                CreatedAt = baseInfo.CreatedAt,
                ServiceScope = baseInfo.ServiceScope,
                StartDate = DateTime.Now.Date
            },

            WorkType.LLP => new LLPWorkAssignment
            {
                ForYear = baseInfo.ForYear,
                ClientId = baseInfo.CompanyId,
                WorkType = baseInfo.WorkType,
                CreatedAt = baseInfo.CreatedAt,
                ServiceScope = baseInfo.ServiceScope,

            },

            _ => throw new NotSupportedException($"WorkType '{request.Type}' not supported")
        };
    }
}
