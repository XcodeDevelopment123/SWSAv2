using AutoMapper;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.CompanyStaffs.Profiles;

public class CompanyStaffProfile : Profile
{
    public CompanyStaffProfile()
    {
        CreateMap<CompanyStaff, CompanyStaff>();
    }
}