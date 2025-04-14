using AutoMapper;
using SWSA.MvcPortal.Dtos.Requests.CompanyWorks;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.CompnayWorks.Profiles;

public class CompanyWorkProgressProfile:Profile
{
    public CompanyWorkProgressProfile()
    {
        CreateMap<CreateCompanyWorkProgressRequest, CompanyWorkProgress>();
    }
}
