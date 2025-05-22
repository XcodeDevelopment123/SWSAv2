using AutoMapper;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Companies.Profiles;

public class CompanyMsicCodeProfile : Profile
{
    public CompanyMsicCodeProfile()
    {
        CreateMap<CompanyMsicCode, CompanyMsicCode>();
        CreateMap<CompanyMsicCode, CompanyMsicCodeVM>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.MsicCode.Code))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.MsicCode.Description))
            .ForMember(dest => dest.CategoryReferences, opt => opt.MapFrom(src => src.MsicCode.CategoryReferences));
    }
}
