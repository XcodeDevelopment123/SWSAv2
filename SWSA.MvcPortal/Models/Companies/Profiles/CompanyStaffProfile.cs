using AutoMapper;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Companies.Profiles;

public class CompanyStaffProfile : Profile
{
    public CompanyStaffProfile()
    {
        CreateMap<CreateCompanyStaffRequest, CompanyStaff>();
    }
}
