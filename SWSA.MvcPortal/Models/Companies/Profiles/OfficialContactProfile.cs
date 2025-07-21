using AutoMapper;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Companies.Profiles;

public class OfficialContactProfile : Profile
{
    public OfficialContactProfile()
    {
        CreateMap<OfficialContact, OfficialContact>();
    }
}
