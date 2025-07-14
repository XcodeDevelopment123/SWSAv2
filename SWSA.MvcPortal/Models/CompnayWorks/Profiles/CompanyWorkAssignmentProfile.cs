using AutoMapper;
using SWSA.MvcPortal.Dtos.Requests.CompanyWorks;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.CompnayWorks.Profiles;

public class CompanyWorkAssignmentProfile : Profile
{
    public CompanyWorkAssignmentProfile()
    {
        CreateMap<WorkAssignment, WorkAssignment>();
        CreateMap<CreateCompanyWorkAssignmentRequest, WorkAssignment>();
    }
}
