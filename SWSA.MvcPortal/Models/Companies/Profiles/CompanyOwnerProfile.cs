using AutoMapper;
using SWSA.MvcPortal.Entities.Contacts;

namespace SWSA.MvcPortal.Models.Companies.Profiles;

public class CompanyOwnerProfile : Profile
{
    public CompanyOwnerProfile()
    {
        CreateMap<CompanyOwner, CompanyOwner>();
    }
}

