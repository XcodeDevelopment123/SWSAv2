using AutoMapper;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Companies.Profiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, Company>();
      
        CreateMap<Company, CompanySelectionVM>()
               .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id));

        CreateMap<CreateCompanyRequest, Company>()
            .ForMember(dest => dest.MsicCodes, opt => opt.MapFrom(src => src.MsicCodeIds.Select(id => new CompanyMsicCode(id)).ToList()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.ComplianceDate, opt => opt.MapFrom(src => src.ComplianceDate))
            .ForMember(dest => dest.Owners, opt => opt.MapFrom(src => src.CompanyOwners));

        CreateMap<Company,CompanySimpleInfoVM>()
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Name));

    }
}
