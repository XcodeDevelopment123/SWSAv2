using AutoMapper;
using SWSA.MvcPortal.Dtos.Requests.CompanyWorks;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.CompnayWorks.Profiles;

public class CompanyWorkProgressProfile:Profile
{
    public CompanyWorkProgressProfile()
    {
        CreateMap<CreateCompanyWorkProgressRequest, CompanyWorkProgress>();
        CreateMap<CompanyWorkProgress, CompanyWorkProgressVM>()
            .ForMember(dest => dest.ProgressId, opt => opt.MapFrom(src => src.Id))
;
    }
}
