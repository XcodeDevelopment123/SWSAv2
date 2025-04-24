using AutoMapper;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.CompanyStaffs.Profiles;

public class CompanyStaffProfile : Profile
{
    public CompanyStaffProfile()
    {
        CreateMap<CompanyStaff, CompanyStaff>();
        CreateMap<CompanyStaff, CompanyStaffVM>()
            .ForMember(dest => dest.HasPassword, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.HashedPassword)));


    }
}