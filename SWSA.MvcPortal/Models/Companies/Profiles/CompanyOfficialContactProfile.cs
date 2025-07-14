using AutoMapper;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Companies.Profiles;

public class CompanyOfficialContactProfile : Profile
{
    public CompanyOfficialContactProfile()
    {
        CreateMap<OfficialContact, OfficialContact>();
        CreateMap<CreateCompanyOfficialContactRequest, OfficialContact>();
    }
}
